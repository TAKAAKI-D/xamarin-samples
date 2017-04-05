﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTodoXForms.Models
{
    /// <summary>
    /// フィルターができるコレクションクラス
    /// </summary>
    public class ToDoFiltableCollection : ObservableCollection<ToDo>
    {
        // 元のリストデータ
        private List<ToDo> _items;
        // ソート用の項目
        bool _dispComplted = true;
        int _sortOrder = 0;

        public ToDoFiltableCollection() : base()
        {
            _items = new List<ToDo>();
        }
        public ToDoFiltableCollection(List<ToDo> items) : base(items)
        {
            _items = items;
        }

        /// <summary>
        /// 項目を追加する
        /// </summary>
        /// <param name="item"></param>
        public new void Add(ToDo item)
        {
            _items.Add(item);
            UpdateFilter();
        }
        /// <summary>
        /// 項目を削除する
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public new bool Remove(ToDo item)
        {
            bool b = _items.Remove(item);
            this.Remove(item);
            return b;
        }
        /// <summary>
        /// 位置を指定して項目を追加する
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public new void Insert(int index, ToDo item)
        {
            _items.Insert(index, item);
            UpdateFilter();
        }

        /// <summary>
        /// IDを指定して項目を更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void Update( int id, ToDo item )
        {
            var it = _items.First(x => x.Id == id);
            if ( it != null )
            {
                item.Copy(it);
                UpdateFilter();
            }
        }

        /// <summary>
        /// ソート状態のアップデート
        /// </summary>
        public void UpdateFilter()
        {
            // ソートを反映させる
            SetFilter(_dispComplted, _sortOrder);
        }

        public void SetFilter(bool dispCompleted, int sortOrder)
        {
            _dispComplted = dispCompleted;
            _sortOrder = sortOrder;

            List<ToDo> lst = _items;
            switch (sortOrder)
            {
                case 0: // 作成日順/ID順
                    lst = _items.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case 1: // 項目名順
                    lst = _items.OrderBy(x => x.Text).ToList();
                    break;
                case 2: // 期日順
                    lst = _items.OrderBy(x => x.DueDate).ToList();
                    break;
            }
            // 未完了だけを表示する
            if (dispCompleted == false)
            {
                lst = lst.Where(x => x.Completed == false).ToList();
            }
            // 全てを追加し直す
            this.Clear();
            lst.All(x => { base.Add(x); return true; });
        }
    }
}

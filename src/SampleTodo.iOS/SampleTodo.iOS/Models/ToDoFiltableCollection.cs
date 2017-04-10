using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SampleTodo.iOS.Models
{
    /// <summary>
    /// �t�B���^�[���ł���R���N�V�����N���X
    /// </summary>
    public class ToDoFiltableCollection : ObservableCollection<ToDo>
    {
        // ���̃��X�g�f�[�^
        private List<ToDo> _items;
        // �\�[�g�p�̍���
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
        public new void Add(ToDo item)
        {
            _items.Add(item);
            SetFilter(_dispComplted, _sortOrder);
        }
        public new bool Remove(ToDo item)
        {
            bool b = _items.Remove(item);
            this.Remove(item);
            return b;
        }
        public new void Insert(int index, ToDo item)
        {
            _items.Insert(index, item);
            SetFilter(_dispComplted, _sortOrder);
        }

        /// <summary>
        /// ID���w�肵�č��ڂ��X�V����
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        public void Update(int id, ToDo item)
        {
            var it = _items.First(x => x.Id == id);
            if (it != null)
            {
                item.Copy(it);
                UpdateFilter();
            }
        }

        /// <summary>
        /// �\�[�g��Ԃ̃A�b�v�f�[�g
        /// </summary>
        public void UpdateFilter()
        {
            // �\�[�g�𔽉f������
            SetFilter(_dispComplted, _sortOrder);
        }

        public void SetFilter(bool dispCompleted, int sortOrder)
        {
            _dispComplted = dispCompleted;
            _sortOrder = sortOrder;

            List<ToDo> lst = _items;
            switch (sortOrder)
            {
                case 0: // �쐬����/ID��
                    lst = _items.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case 1: // ���ږ���
                    lst = _items.OrderBy(x => x.Text).ToList();
                    break;
                case 2: // ������
                    lst = _items.OrderBy(x => x.DueDate).ToList();
                    break;
            }
            // ������������\������
            if (dispCompleted == false)
            {
                lst = lst.Where(x => x.Completed == false).ToList();
            }
            // �S�Ă�ǉ�������
            this.Clear();
            lst.All(x => { base.Add(x); return true; });
        }
    }
}
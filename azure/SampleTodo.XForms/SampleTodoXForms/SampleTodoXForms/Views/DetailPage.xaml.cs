﻿using SampleTodoXForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleTodoXForms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage()
        {
            InitializeComponent();
        }
        public DetailPage(ToDo item )
        {
            InitializeComponent();
            this.BindingContext = _item = item.Copy();
            this._item_org = item;
        }

        ToDo _item, _item_org;

        /// <summary>
        /// 保存ボタンをタップ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // 元に戻る
            await this.Navigation.PopAsync();
            // 変更結果を保存する
            if (swDue.IsToggled == true)
            {
                _item.DueDate = dpDue.Date;
            }
            else
            {
                _item.DueDate = null;
            }

            if (_item.Id == "")
            {
                // 項目を追加
                MessagingCenter.Send(this, "AddItem", _item);
            }
            else
            {
                // メイン画面から渡されたデータを更新する
                _item.Copy(_item_org);
                // 既存項目の更新
                MessagingCenter.Send(this, "UpdateItem", _item_org);
            }
        }
        /// <summary>
        /// 戻るボタンを押したとき
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // 保存せず前の画面に戻る
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// 期日のトグルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if ( swDue.IsToggled )
            {
                if (_item.DueDate == null)
                {
                    _item.DueDate = DateTime.Now;
                }
                dpDue.IsVisible = true;
            }
            else
            {
                _item.DueDate = null;
                dpDue.IsVisible = false;
            }
        }
    }
}

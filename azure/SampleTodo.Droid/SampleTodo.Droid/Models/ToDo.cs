using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SampleTodo.Droid.Helpers;

namespace SampleTodo.Droid.Models
{
    /// <summary>
    /// ToDo �̃A�C�e���N���X
    /// </summary>
    public class ToDo : ObservableObject
    {
        // ���j�[�NID
        public string Id { get; set; }
        // ���ږ�
        string text;
        public string Text
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        // ����
        DateTime? dueDate;
        public DateTime? DueDate
        {
            get { return dueDate; }
            set
            {
                SetProperty(ref dueDate, value);
                this.OnPropertyChanged("DispDueDate");
                this.OnPropertyChanged("StrDueDate");
            }
        }
        public bool UseDueDate
        {
            get
            {
                return this.DueDate != null;
            }
        }
        public DateTime DispDueDate
        {
            get
            {
                return this.DueDate == null ? DateTime.Now : DueDate.Value;
            }
        }
        public string StrDueDate
        {
            get
            {
                return this.DueDate == null ? "" : DueDate.Value.ToString("yyyy-MM-dd");
            }
        }
        // ����
        public bool Completed { get; set; }
        // �쐬��
        public DateTime CreatedAt { get; set; }


        /// <summary>
        /// �ҏW�p�̃R�s�[���\�b�h
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public ToDo Copy(ToDo target = null)
        {
            if (target == null)
            {
                target = new ToDo();
            }
            target.Id = this.Id;
            target.text = this.text;
            target.DueDate = this.DueDate;
            target.Completed = this.Completed;
            target.CreatedAt = this.CreatedAt;
            return target;
        }
    }

    /// <summary>
    /// �ݒ�N���X
    /// </summary>
    public class Setting
    {
        // �����̕\��
        public bool DispCompleted { get; set; }
        // �\���� (0:�쐬��, 1:���ږ���, 2:������)
        public int SortOrder { get; set; }
    }
}
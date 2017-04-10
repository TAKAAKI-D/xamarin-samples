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

namespace SampleTodo.Droid
{
    [Activity(Label = "SettingActivity")]
    public class SettingActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Setting);

            // �f�[�^���󂯎��
            var dispCompleted = Intent.GetBooleanExtra("DispCompleted", true);
            var sortOrder = Intent.GetIntExtra("SortOrder", 0);

            spOrder = FindViewById<Spinner>(Resource.Id.spOrder);
            string[] items = { "�쐬����", "���ږ���", "������" };
            var ad = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, items);
            spOrder.Adapter = ad;
            spOrder.ItemSelected += SpSort_ItemSelected;

            swDispCompeted = FindViewById<Switch>(Resource.Id.swDispCompleted);
            swDispCompeted.Checked = dispCompleted;
            spOrder.SetSelection(sortOrder);

            var btnBack = FindViewById<Button>(Resource.Id.buttonBack);
            btnBack.Click += BtnBack_Click;

        }

        Switch swDispCompeted;
        Spinner spOrder;


        /// <summary>
        /// �O�̉�ʂɖ߂�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            var intent = new Intent();
            intent.PutExtra("DispCompleted", swDispCompeted.Checked);
            intent.PutExtra("SortOrder", spOrder.SelectedItemPosition);
            SetResult(Result.Ok, intent);
            Finish();
        }

        /// <summary>
        /// �߂�{�^�����^�b�v�����Ƃ�
        /// </summary>
        public override void OnBackPressed()
        {
            var intent = new Intent();
            intent.PutExtra("DispCompleted", swDispCompeted.Checked);
            intent.PutExtra("SortOrder", spOrder.SelectedItemPosition);
            SetResult(Result.Ok, intent);
            Finish();
        }

        /// <summary>
        /// �\�[�g���̍��ڂ�I�������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpSort_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            int pos = e.Position;

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagement.Models;
using TMPro;

namespace UI.Views
{

	public class ProductView : ModelView<Product>
	{
		[SerializeField]
		private TMP_Text nameText = null;

		[SerializeField]
		private TMP_Text descriptionText = null;

		[SerializeField]
		private TMP_Text priceText = null;

		public override void View(Product model)
		{
			nameText.text = model.name;
			descriptionText.text = model.description;
			priceText.text = model.price + "";
		}

		public override void UpdateView(Product model)
		{
			throw new System.NotImplementedException();
		}
	}

}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.ViewModel
{
	public class InfoViewModel : NotifyViewModelBase<string>
	{
		public InfoViewModel()
		{
			Title = "Bilgilendirme";
		}
	}
}
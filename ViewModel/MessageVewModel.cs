using AddSharedParametersToFamily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddSharedParametersToFamily.View;
using AddSharedParametersToFamily.ViewModel;
using AddSharedParametersToFamily.MVVM;

namespace AddSharedParametersToFamily.ViewModel

{
    public class MessageVewModel
    {
        internal RevitModelClass RevitModel { get; set; }
        public MessageVewModel(RevitModelClass rm_)

        {
            RevitModel = rm_;

        }
    }
}

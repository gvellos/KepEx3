using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEP
{
    internal class Requests
    {
        public String HandleRequest(String requestType)
        {
            switch (requestType)
            {
                case "Βεβαίωση":
                    return "Η βεβαίωση σας θα εκδοθεί συντόμα";                
                case "Εξουσιοδότηση":
                    return "Η εξουσιοδότηση σας θα εκδοθεί συντόμα";
                default:
                    return "Δεν αναγνωρίστηκε ο τύπος του εγγράφου";
            }
        }
    }
}

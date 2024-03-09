using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawnShop.Infrastructure.Data.Constants
{
    public static class DataConstants
    {
        public const int ProductNameMaxLength = 20;
        public const int ProductNameMinLength = 3;

        public const int ProductDescriptionMaxLength = 200;
        public const int ProductDescriptionMinLength = 20;

        public const double ProductMaxWeight = 3500;
        public const double ProductMinWeight = 0.01;

        public const int CategoryNameMaxLength = 20;
        public const int CategoryNameMinLength = 3;

        public const int PossessionNameMaxLength = 25;
        public const int PossessionNameMinLength = 3;

        public const double PossessionMaxArea = 1_000_000;
        public const double PossessionMinArea = 10_000;

        public const int PossessionLocationMaxLength = 100;
        public const int PossessionLocationMinLength = 10;
    }
}

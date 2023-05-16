using FluentValidation;
using RoomManagement.Areas.Admin.Models.RoomModel;

namespace RoomManagement.Validations
{
    public class RoomValidator : AbstractValidator<RoomEditModel>
    {
        public RoomValidator() 
        { 

        }
    }
}


namespace lmsapp.webui.Models
{
    public class EnrollmentListModel
    {
        public int EnrollmentId { get; set; }
        public string EnrollmentNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<EnrollmentItemModel> EnrollmentItems { get; set; }
        public decimal TotalPrice()
        {
            return EnrollmentItems.Sum(i => i.Price * i.Quantity);
        }
    }

    public class EnrollmentItemModel
    {
        public int EnrollmentItemId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
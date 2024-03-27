namespace University.ViewModels
{
    public class StudentInfoVM : ViewModelBase
    {
        private readonly IReviewService _reviewService;
        private readonly ApplicationDbContext _appDBContext;

        private ObservableCollection<Review> _studentReviewList;

        public ObservableCollection<Review> Reviews
        {
            get { return _studentReviewList; }
            set { 
                _studentReviewList = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }

        private DelegateCommand _addReviewCommand;
        private DelegateCommand<Review> _removeReviewCommand;

        public DelegateCommand<Review> RemoveReviewCommand =>
            _removeReviewCommand ?? (_removeReviewCommand = new DelegateCommand<Review>(ExecuteRemoveReviewCommand));
        public DelegateCommand AddReviewCommand =>
                _addReviewCommand ?? (_addReviewCommand = new DelegateCommand(ExecuteAddReviewCommand));

        public StudentInfoVM(ApplicationDbContext appDBContext, int studentId)
        {
            _appDBContext = appDBContext;
            _reviewService = new ReviewService(new ReviewRepository(_appDBContext));

            LoadDataAsync(studentId);
        }

        private async Task LoadDataAsync(int studentId)
        {
            try
            {
                var reviews = await _reviewService.ListByStudentIdAsync(studentId);

                Reviews = new ObservableCollection<Review>(reviews);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExecuteAddReviewCommand()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteRemoveReviewCommand(Review review)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this comment?", "Confirmation",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await _reviewService.DeleteAsync(review.Id);
                    Reviews.Remove(review);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

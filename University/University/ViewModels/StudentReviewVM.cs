namespace University.ViewModels
{
    public class StudentReviewVM : ViewModelBase
    {
        private readonly IReviewService _reviewService;

        private ObservableCollection<Review> _studentReviewList;
        private DateTime _postDate;
        private int _teacherId;
        private int _studentId;
        private string _feedBack;

        public ObservableCollection<Review> Reviews
        {
            get { return _studentReviewList; }
            set { 
                _studentReviewList = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }
        public string FeedBack
        {
            get { return _feedBack; }
            set
            {
                _feedBack = value;
                OnPropertyChanged(nameof(FeedBack));
            }
        }

        private DelegateCommand _addReviewCommand;
        private DelegateCommand<Review> _removeReviewCommand;

        public DelegateCommand<Review> RemoveReviewCommand =>
            _removeReviewCommand ?? (_removeReviewCommand = new DelegateCommand<Review>(ExecuteRemoveReviewCommand));
        public DelegateCommand AddReviewCommand =>
                _addReviewCommand ?? (_addReviewCommand = new DelegateCommand(ExecuteAddReviewCommand));

        public StudentReviewVM(ApplicationDbContext appDBContext, int studentId, int teacherId)
        {
            _studentId = studentId;
            _teacherId = teacherId;

            _reviewService = new ReviewService(new ReviewRepository(appDBContext));

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var reviews = await _reviewService.ListByStudentIdAsync(_studentId);

                Reviews = new ObservableCollection<Review>(reviews);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void ExecuteAddReviewCommand()
        {
            try
            {
                if (string.IsNullOrEmpty(_feedBack))
                    throw new ArgumentException("FeedBack is empty");

                var newReview = new Review()
                {
                    FeedBack = _feedBack,
                    PostDate = DateTime.Now,
                    TeacherId = _teacherId,
                    StudentId = _studentId,
                };

                await _reviewService.AddAsync(newReview);
                await LoadDataAsync();
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

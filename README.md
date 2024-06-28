# learningManagementSystem

## Authentication System

### Register
- You can register to the app with your email if you don't have an account.
- You can register as an Instructor or as a Student.
- Once registered by email, check your inbox to verify the account using the sent OTP code.
- Enter the OTP code in the redirected form to confirm the email.
- If the OTP expired, you can request a new one.
- Once you verify your email, you will be logged in automatically.

### Login
- You can log in to the app if the session expired.
- If you forget your password, you can request to update it.
- Once requested, the system sends a verification link to update the password.
- The link redirects you to the update password form.

### Once logged in to our app, you can perform the following actions on your account (Account Management)
- Update account password
- Update account info like (UserName, Address, etc.)
- you can Logout from the App
- Delete account (you must have the account password to delete it)
- If you are an instructor, you can't delete the account if there are students enrolled in your courses.

---

## The System Has Three Roles: Admin, Instructor, Student

### Admin Role
A user with the Admin role can perform the following operations:
- Create, update, and delete categories.
- Delete any course (the course must have no students).
- Assign Admin role to any user from the Admin Dashboard.
- Create and delete roles from the Admin Dashboard.
- Access any user by email and view information (except password).
- Retrieve orders created in the last month.
- Retrieve orders created in the last year.
- Retrieve courses created in the last month.
- Retrieve courses created in the last year.
- Block and unblock users.
- can create course and make all operations that allowed to Instructor.
- can make all operations that allowed in (Instructor) role.
- Access system info in JSON format:
```
  {
    "coursesCountOfLastMonth": 0,
    "coursesCountOfLastYear": 0,
    "ordersCountOfLastMonth": 0,
    "ordersCountOfLastYear": 0,
    "allCategoriesCount": 0,
    "allCoursesCount": 0,
    "allOrdersCount": 0,
    "blockedUserCount": 0,
    "unBlockedUserCount": 0,
    "appUsersCount": 0,
    "instructorsCount": 0,
    "studentCount": 0,
    "adminsCount": 0
  }
```

---

# User Roles and Operations

## Instructor Role
A user with the Instructor role can perform the following operations:
- Create, update, and delete courses.
- Cannot delete courses if there are students enrolled in them.
- Create, update, and delete lessons within the course.
- Upload videos to the created lessons.
- Update and delete videos.
- Lock and unlock videos.
- Enroll any student in a course manually.
- Remove any student from a course manually.
- Search for any course.
- Perform all operations allowed to Students.

## Student Role
A user with the Student role can perform the following operations:
- Browse any categories.
- Filter categories by (Date, Price, Alphabetic).
- Browse any course.
- Filter courses by (Date, Price, Alphabetic).
- Click on a course to view course details.
- If the student does not have access to the course, they cannot watch any videos of the course.
- Purchase courses using a card via the [Stripe] payment gateway.
- Once the payment is successful, the system enrolls the user in the course automatically and unlocks the course videos.
- The system sends an email with course details upon successful enrollment.
- Add comments to courses and reply to other comments.
- Delete and edit their own comments.
- Access all courses they have paid for.

  

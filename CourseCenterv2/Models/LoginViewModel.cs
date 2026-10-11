
using System.ComponentModel.DataAnnotations;

namespace CourseCenterv2.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "اكتب اسم المستخدم أو البريد الإلكتروني")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "اكتب كلمة المرور")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}
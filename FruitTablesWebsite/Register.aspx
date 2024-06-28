<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="FruitTablesWebsite.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Register Page</title>
    <link rel="stylesheet" href="css/stylesregister.css">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="wrapper">

                <h1>Register</h1>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxUsernameRegister" runat="server" placeholder="Username"></asp:TextBox>


                    <i class='bx bxs-user'></i>
                </div>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxEmailRegister" runat="server" placeholder="Email"></asp:TextBox>


                    <i class='bx bxs-user'></i>
                </div>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxPasswordRegister" runat="server" placeholder="Password"></asp:TextBox>


                    <i class='bx bxs-user'></i>
                </div>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxConfirmPassRegister" runat="server" placeholder=" Confirm Password"></asp:TextBox>

                    <i class='bx bxs-lock-alt'></i>
                </div>
                <div class="remember-forgot">
                    <label>
                        <input type="checkbox">Remember Me</label>
                    <a href="#">
                        <asp:Button ID="Button3" runat="server" Text="Forgot Password" class="btn-link" /></a>
                </div>
                <asp:Button ID="ButtonRegister" runat="server" Text="Register" class="btn" OnClick="ButtonRegister_Click" />

                <div class="register-link">
                    <p>
                        Already have an account?
                        <asp:Button ID="Button2" runat="server" Text="Login" class="btn-link" OnClick="Button2_Click" />
                    </p>
                </div>

            </div>
        </div>
    </form>
</body>
</html>

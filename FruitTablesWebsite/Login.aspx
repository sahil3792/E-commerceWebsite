<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FruitTablesWebsite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login Page</title>
    <link rel="stylesheet" href="css/styles.css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            
                <h1>Login</h1>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxUsernameLogin" runat="server" placeholder="Username"></asp:TextBox>
                    

                    <i class='bx bxs-user'></i>
                </div>
                <div class="input-box">
                    <asp:TextBox ID="TextBoxPasswordLogin" runat="server" placeholder="Password"></asp:TextBox>
                    
                    <i class='bx bxs-lock-alt'></i>
                </div>
                <div class="remember-forgot">
                    <label>
                        <input type="checkbox">Remember Me</label>
                    <a href="#">
                        <asp:Button ID="Button3" runat="server" Text="Forgot Password" class="btn-link" /></a>
                </div>
            <asp:Button ID="ButtonLogin" runat="server" Text="Login" class="btn" OnClick="ButtonLogin_Click"  />
                
                <div class="register-link">
                    <p>Dont have an account? <asp:Button ID="Button2" runat="server" Text="Register" class="btn-link" OnClick="Button2_Click" /></p>
                </div>
            
        </div>
    </form>
</body>
</html>

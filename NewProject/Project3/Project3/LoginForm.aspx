<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginForm.aspx.cs" Inherits="Project3.LoginForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 40%; margin: auto; border: 1px solid gray; padding: 20px; margin-top: 50px; border-radius: 10px">

            <div class="form-group">
                <label for="exampleInputEmail1">Email address</label>
                <asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Enter email"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="exampleInputPassword1">Password</label>
                <asp:TextBox ID="TextBox2" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
            </div>
            <div class="form-check">
                <asp:Button ID="Button2" runat="server" Text="New Employee" Style="border: none; background-color: none; color: blue; text-decoration: underline" OnClick="Button2_Click" />
            </div>
            <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-primary" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>

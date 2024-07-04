<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetPassword.aspx.cs" Inherits="Project3.SetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
                <div class="form-group">
                    <label for="exampleInputEmail1">Email address</label>

                    <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">Password</label>

                    <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="exampleInputEmail1">Confirm Password</label>

                    <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
                </div>

            <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-primary" OnClick="Button1_Click" />
                
           
        </div>
    </form>
</body>
</html>

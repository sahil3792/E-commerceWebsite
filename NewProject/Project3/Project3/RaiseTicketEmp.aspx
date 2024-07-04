<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="RaiseTicketEmp.aspx.cs" Inherits="Project3.RaiseTicketEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="form-group">
            <label for="exampleFormControlInput1">Designation</label>
            <asp:TextBox ID="TextBox1" class="form-control" runat="server" placeholder="Ex. Trainee"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleFormControlSelect1">Raise Ticket To:</label>
            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label for="exampleFormControlTextarea1">Ticket</label>
            <asp:TextBox ID="TextBox2" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="exampleFormControlTextarea1">Attachments</label>
            <asp:FileUpload ID="FileUpload1" runat="server" class="form-control" />
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Raise Ticket" class="btn btn-primary" OnClick="Button1_Click" />
        </div>
    </div>

</asp:Content>

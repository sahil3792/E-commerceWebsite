<%@ Page Title="" Language="C#" MasterPageFile="~/HRMaster.Master" AutoEventWireup="true" CodeBehind="HrJoiningForm.aspx.cs" Inherits="Project2.HrJoiningForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" integrity="sha384-VaMv3jWRSBnsrux1eAL7iTXJKRtDzH0EuZWhtOUlHJZyjT+cVndOby+57HHfMYka" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js" integrity="sha384-lfVrsSxydHEu3cINLknV2ShH9ri7TBpTwZ7h4bnQAEV3O1M1YymJFFJnPi+4KJpM" crossorigin="anonymous"></script>
    <title></title>

</asp:Content>
    
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
        <br />
        <br />
        <div class="container" style="width:70%; border:2px solid gray; padding:30px; border-radius:10px">
            <div class="form-group">
                <label for="TextBox1">Name</label>
                <asp:TextBox ID="TextBox1" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox2">Contact</label>
                <asp:TextBox ID="TextBox2" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox3">Email</label>
                <asp:TextBox ID="TextBox3" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="TextBox4">Salary</label>
                <asp:TextBox ID="TextBox4" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label for="Calendar1">DOB </label>
                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                </div>
                <div class="form-group col-md-6">
                    <label for="Calendar2">DOJ </label>
                    <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                </div>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-primary" OnClick="Button1_Click" />
        </div>
    </asp:Content>

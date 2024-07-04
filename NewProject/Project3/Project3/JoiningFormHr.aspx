<%@ Page Title="" Language="C#" MasterPageFile="~/HRMaster.Master" AutoEventWireup="true" CodeBehind="JoiningFormHr.aspx.cs" Inherits="Project3.JoiningFormHr" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
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
    <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-primary" onclick="Button1_Click" />
</div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

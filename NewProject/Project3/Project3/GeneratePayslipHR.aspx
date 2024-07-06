<%@ Page Title="" Language="C#" MasterPageFile="~/HRMaster.Master" AutoEventWireup="true" CodeBehind="GeneratePayslipHR.aspx.cs" Inherits="Project3.GeneratePayslipHR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowCommand="gvEmployees_RowCommand">
        <Columns>
            <asp:BoundField DataField="UserId" HeaderText="User ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:BoundField DataField="DOB" HeaderText="Date of Birth" />
            <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" />
            <asp:BoundField DataField="Contact" HeaderText="Contact" />
            <asp:TemplateField HeaderText="Action">
    <ItemTemplate>
        <asp:Button ID="btnCalculateSalary" runat="server" Text="Calculate Salary" CommandName="CalculateSalary" CommandArgument='<%# Eval("UserID") %>' />
    </ItemTemplate>
</asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

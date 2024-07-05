<%@ Page Title="" Language="C#" MasterPageFile="~/HRMaster.Master" AutoEventWireup="true" CodeBehind="PayslipHR.aspx.cs" Inherits="Project2.PayslipHR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" OnRowCommand="gvEmployees_RowCommand">
        <Columns>
            <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Salary" HeaderText="Salary" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnCalculateSalary" runat="server" Text="Calculate Salary" CommandName="CalculateSalary" CommandArgument='<%# Eval("EmployeeID") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="ViewSolutionEmp.aspx.cs" Inherits="Project3.ViewSolutionEmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" />
            <asp:BoundField DataField="TicketDescription" HeaderText="Ticket Description" />
            <asp:BoundField DataField="Solution" HeaderText="Solution" />
            <asp:BoundField DataField="SolutionDate" HeaderText="Solution Date" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" />
        </Columns>
    </asp:GridView>
</asp:Content>

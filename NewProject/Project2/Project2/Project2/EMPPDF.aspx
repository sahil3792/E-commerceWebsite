<%@ Page Title="" Language="C#" MasterPageFile="~/EMPMaster.Master" AutoEventWireup="true" CodeBehind="EMPPDF.aspx.cs" Inherits="Project2.EMPPDF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <div>
        <h2>Employee Details</h2>
        <asp:Label ID="lblEmployeeName" runat="server" Text="Employee Name:"></asp:Label>
        <br />
        <asp:Label ID="lblEmployeeID" runat="server" Text="Employee ID:"></asp:Label>
        <br />
        <asp:Label ID="lblEmployeeEmail" runat="server" Text="Email:"></asp:Label>
        <br />
        <asp:Label ID="lblEmployeeSalary" runat="server" Text="Salary:"></asp:Label>
        <br />
        <asp:Label ID="lblCalculatedSalary" runat="server" Text="Calculated Salary:"></asp:Label>
        <br />
        <asp:Label ID="lblPaidLeavesTaken" runat="server" Text="Paid Leaves Taken:"></asp:Label>
        <br />
        <asp:Label ID="lblCasualLeavesTaken" runat="server" Text="Casual Leaves Taken:"></asp:Label>
        <br />
        <asp:Label ID="lblSickLeavesTaken" runat="server" Text="Sick Leaves Taken:"></asp:Label>
        <br />
        <asp:Label ID="lblPaidLeavesLeft" runat="server" Text="Paid Leaves Left:"></asp:Label>
        <br />
        <asp:Label ID="lblCasualLeavesLeft" runat="server" Text="Casual Leaves Left:"></asp:Label>
        <br />
        <asp:Label ID="lblSickLeavesLeft" runat="server" Text="Sick Leaves Left:"></asp:Label>
        <br />
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
         <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="btnDownloadPDF" runat="server" Text="Download as PDF" OnClick="btnDownloadPDF_Click" />
    </div>

</asp:Content>

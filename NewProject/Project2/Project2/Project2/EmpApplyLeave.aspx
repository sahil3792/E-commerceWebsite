<%@ Page Title="" Language="C#" MasterPageFile="~/EMPMaster.Master" AutoEventWireup="true" CodeBehind="EmpApplyLeave.aspx.cs" Inherits="Project2.EmpApplyLeave" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <br />
    <asp:Label ID="lblLeaveType" runat="server" Text="Leave Type:"></asp:Label>
    <asp:DropDownList ID="ddlLeaveType" runat="server">
        <asp:ListItem Text="Select Leave Type" Value=""></asp:ListItem>
        <asp:ListItem Text="Sick Leave" Value="Sick Leave"></asp:ListItem>
        <asp:ListItem Text="Casual Leave" Value="Casual Leave"></asp:ListItem>
        <asp:ListItem Text="Paid Leave" Value="Paid Leave"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>
    <br />
    <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd"></ajaxToolkit:CalendarExtender>
    <br />
    <asp:Label ID="lblReason" runat="server" Text="Reason:"></asp:Label>
    <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="4" Columns="40"></asp:TextBox>
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
    <br />
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>

</asp:Content>

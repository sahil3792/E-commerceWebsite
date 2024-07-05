<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationForm.aspx.cs" Inherits="Project3.LeaveApplicationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
        <div class="form-group">
            <label for="exampleFormControlInput1">Leave Type</label>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>Sick Leave</asp:ListItem>
                <asp:ListItem>Casual Leave</asp:ListItem>
                <asp:ListItem>Paid Leave</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
            <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtStartDate" format="yyyy-MM-dd"></ajaxtoolkit:calendarextender>

        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="End Date:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtStartDate" format="yyyy-MM-dd"></ajaxtoolkit:calendarextender>

        </div>
        
        <div class="form-group">
            <label for="exampleFormControlTextarea1">Reason</label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click"   />
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
    
</asp:Content>

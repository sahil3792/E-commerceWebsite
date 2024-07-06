<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationForm.aspx.cs" Inherits="Project3.LeaveApplicationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width:40%; border:1px solid gray;margin:auto;padding:20px; margin-top:20px;border-radius:10px">
        <div class="form-group">
            <label for="exampleFormControlInput1">Leave Type</label>
            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                <asp:ListItem>Sick Leave</asp:ListItem>
                <asp:ListItem>Casual Leave</asp:ListItem>
                <asp:ListItem>Paid Leave</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:Label ID="lblStartDate" runat="server"  Text="Start Date:"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server" class="form-control"></asp:TextBox>
            <ajaxtoolkit:calendarextender id="CalendarExtender1" runat="server" targetcontrolid="txtStartDate" format="yyyy-MM-dd"></ajaxtoolkit:calendarextender>

        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="End Date:" ></asp:Label>
            <asp:TextBox ID="txtEndDate" runat="server" class="form-control"></asp:TextBox>
            <ajaxtoolkit:calendarextender id="CalendarExtender2" runat="server" targetcontrolid="txtEndDate" format="yyyy-MM-dd"></ajaxtoolkit:calendarextender>

        </div>
        
        <div class="form-group">
            <label for="exampleFormControlTextarea1">Reason</label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
        </div>
        <div>
        <asp:Button ID="Button1" runat="server" Text="Apply Leave" onclick="Button1_Click"  class="btn btn-primary" />
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
    </div>
</asp:Content>

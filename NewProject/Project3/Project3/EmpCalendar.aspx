<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="EmpCalendar.aspx.cs" Inherits="Project3.EmpCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="calendar"></div>
    <script>
        $(document).ready(function () {
            $('#calendar').fullCalendar({
                events: function (start, end, timezone, callback) {
                    $.ajax({
                        url: '<%= ResolveUrl("~/HrCalendar.aspx/GetEvents") %>',
                        type: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            var events = JSON.parse(data.d);
                            callback(events);
                        }
                    });
                }
            });
        });
    </script>
</asp:Content>

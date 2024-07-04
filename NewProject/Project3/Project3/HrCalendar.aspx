<%@ Page Title="" Language="C#" MasterPageFile="~/HRMaster.Master" AutoEventWireup="true" CodeBehind="HrCalendar.aspx.cs" Inherits="Project3.HrCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.10.2/fullcalendar.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div id="calendar"></div>
    
   <script>
       $(document).ready(function () {
           $('#calendar').fullCalendar({
               editable: true,
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
                },
                selectable: true,
                selectHelper: true,
                select: function (start, end) {
                    var title = prompt('Event Title:');
                    if (title) {
                        var eventData = {
                            title: title,
                            start: start.format(),
                            end: end.format()
                        };
                        $.ajax({
                            url: '<%= ResolveUrl("~/HrCalendar.aspx/AddEvent") %>',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ title: title, startDate: start.format(), endDate: end.format(), description: '' }),
                            success: function () {
                                $('#calendar').fullCalendar('renderEvent', eventData, true);
                                $('#calendar').fullCalendar('unselect');
                            }
                        });
                    }
                },
                eventClick: function (event) {
                    if (confirm("Do you really want to delete this event?")) {
                        $.ajax({
                            url: '<%= ResolveUrl("~/HrCalendar.aspx/RemoveEvent") %>',
                            type: 'POST',
                            contentType: 'application/json; charset=utf-8',
                            data: JSON.stringify({ eventID: event.id }),
                            success: function () {
                                $('#calendar').fullCalendar('removeEvents', event._id);
                            }
                        });
                    }
                }
            });
        });
    </script>
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>

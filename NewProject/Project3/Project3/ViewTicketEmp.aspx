<%@ Page Title="" Language="C#" MasterPageFile="~/EmpMaster.Master" AutoEventWireup="true" CodeBehind="ViewTicketEmp.aspx.cs" Inherits="Project3.ViewTicketEmp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" />
            <asp:BoundField DataField="Designation" HeaderText="Designation" />
            <asp:BoundField DataField="TicketDescription" HeaderText="Ticket Description" />
            <asp:TemplateField HeaderText="Attachment">
                <ItemTemplate>
                    <asp:Button ID="DownloadButton" runat="server" Text="Download" CommandName="Download" CommandArgument='<%# Eval("AttachmentFileName") %>' CssClass="btn btn-link" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RaisedBy" HeaderText="Raised By" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Created At" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="SolutionButton" runat="server" Text="Solution" CommandName="Solution" CommandArgument='<%# Eval("TicketID") %>' CssClass="btn btn-primary" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Solution Modal -->
    <div class="modal fade" id="solutionModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Submit Solution</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:TextBox ID="SolutionTextBox" runat="server" TextMode="MultiLine" CssClass="form-control" Rows="5"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="SubmitSolutionButton" runat="server" Text="Submit" CssClass="btn btn-primary" OnClick="SubmitSolutionButton_Click" />
                    <asp:HiddenField ID="HiddenTicketID" runat="server" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script type="text/javascript">
        function showModal(ticketID) {
            $('#<%= HiddenTicketID.ClientID %>').val(ticketID);
            $('#solutionModal').modal('show');
        }
    </script>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminProductList.aspx.cs" Inherits="FruitTablesWebsite.AdminProductList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="css/ProductList.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br /><br /><br /><br /><br /><br /><br />
    <div class="container">
            <asp:GridView ID="ProductGridView" runat="server" AutoGenerateColumns="False" CssClass="product-table" OnRowDeleting="ProductGridView_RowDeleting" DataKeyNames="Id">
    <Columns>
        <asp:TemplateField HeaderText="Image">
            <ItemTemplate>
                <img src='<%# Eval("ImagePath") %>' alt="Product Image" class="small-image" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Category" HeaderText="Category" />
        <asp:BoundField DataField="Country_Of_Origin" HeaderText="Country of Origin" />
        <asp:BoundField DataField="Product_Check" HeaderText="Product Check" />
        <asp:BoundField DataField="QuantityInKg" HeaderText="Quantity in Kg" />
        <asp:BoundField DataField="Quality" HeaderText="Quality" />

        <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-primary btn-sm" CommandArgument='<%# Eval("Id") %>' />
                <asp:Button runat="server" CommandName="Delete" Text="Delete" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('Are you sure you want to delete this product?');" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
        </div>
</asp:Content>

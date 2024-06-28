<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="FruitTablesWebsite.AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/AdminHomestyles.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="main">
        <div class="addProductcontainer">
            <div class="form-group">

                <asp:TextBox ID="TextBoxProductName" runat="server" class="form-control" placeholder="Product Name"></asp:TextBox>


            </div>
            <div class="form-group">

                <asp:TextBox ID="TextBoxProductDetails" runat="server" class="form-control" Placeholder="Product Details"></asp:TextBox>


            </div>
            <div class="form-group">

                <asp:TextBox ID="TextBoxCategory" runat="server" class="form-control" Placeholder="Product Category"></asp:TextBox>


            </div>
            <div class="form-group">
                
                <asp:TextBox ID="TextBoxPrice" runat="server" class="form-control" Placeholder="Product Price"></asp:TextBox>


            </div>
            <div class="form-group">
                
                <asp:TextBox ID="TextBoxQuantity" runat="server" class="form-control" Placeholder="Product Quantity"></asp:TextBox>


            </div>
            <div class="form-group">
                
                <asp:TextBox ID="TextBoxOrigin" runat="server" class="form-control" Placeholder="Country of Origin"></asp:TextBox>


            </div>
            <div class="form-group">
                
                <asp:TextBox ID="TextBoxQuality" runat="server" class="form-control" Placeholder="Product Quality"></asp:TextBox>

            </div>
            <div class="form-group">

                <asp:TextBox ID="TextBoxCheck" runat="server" class="form-control" Placeholder="Product Check"></asp:TextBox>

            </div>
            <div class="form-group">
                <asp:FileUpload ID="FileUploadImage" runat="server" />
           
            </div>
            

            <asp:Button ID="Button1" runat="server" Text="Add Product" class="btn btn-primary" OnClick="Button1_Click" />

        </div>
    </div>



</asp:Content>

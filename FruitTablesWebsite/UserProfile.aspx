<%@ Page Title="" Language="C#" MasterPageFile="~/UserMaster.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="FruitTablesWebsite.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/script.js"></script>
    <script type="text/javascript">
        function showContent(section) {
            // Hide all sections
            const sections = document.querySelectorAll('.content-section');
            sections.forEach(sec => sec.classList.remove('active'));
            
            // Show the selected section
            document.getElementById(section).classList.add('active');
        }

        // Initial display
        window.onload = function() {
            document.getElementById('orders').classList.add('active');
        };
    </script>
    <link href="css/stylesuserprofile.css" rel="stylesheet" />
    <link href="css/Userprofilestyles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="container">
        <div class="sidebar">
            <div class="user-info">
                <h2><asp:Label ID="LabelUsername" runat="server" Text=""></asp:Label></h2>
                
                <p>123 Main St, Anytown, USA</p>
                <p>(123) 456-7890</p>
            </div>
            <div class="menu">
                <asp:Button ID="EditProfileButton" class="button" runat="server" Text="Edit Profile" OnClientClick="showContent('profile'); return false;" />
            <asp:Button ID="ChangePasswordButton" class="button" runat="server" Text="Change Password" OnClientClick="showContent('password'); return false;" />
            <asp:Button ID="TrackOrdersButton" class="button" runat="server" Text="Track Orders" OnClientClick="showContent('track-orders'); return false;" />
            <asp:Button ID="MyOrdersButton" class="button" runat="server" Text="My Orders" OnClientClick="showContent('orders'); return false;" />
            <asp:Button ID="OrderHistoryButton" class="button" runat="server" Text="Order History" OnClientClick="showContent('history'); return false;" />
            <asp:Button ID="WishListButton" class="button" runat="server" Text="Wish List" OnClientClick="showContent('wishlist'); return false;" />
            </div>
        </div>
        <div class="content">
            <div id="profile" class="content-section">
                <h2>Edit Profile</h2>
                <p>Profile editing form will be displayed here.</p>
            </div>
            <div id="password" class="content-section">
                <h2>Change Password</h2>
                <p>Password change form will be displayed here.</p>
            </div>
            <div id="track-orders" class="content-section">
                <h2>Track Orders</h2>
                <p>Order tracking information will be displayed here.</p>
            </div>
            <div id="orders" class="content-section active">
                <h2>My Orders</h2>
                <p>Order details will be displayed here.</p>
            </div>
            <div id="history" class="content-section">
                <h2>Order History</h2>
                <p>Order history details will be displayed here.</p>
            </div>
            <div id="wishlist" class="content-section">
                <h2>Wish List</h2>
                <p>Wish list items will be displayed here.</p>
            </div>
        </div>
    </div>
    <script src="script.js"></script>



</asp:Content>

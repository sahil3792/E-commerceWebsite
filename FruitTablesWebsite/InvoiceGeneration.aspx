<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceGeneration.aspx.cs" Inherits="FruitTablesWebsite.InvoiceGeneration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Invoice/css/style.css" rel="stylesheet" />
    <link href="Invoice/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Invoice/js/jquery.min.js"></script>
    <script src="Invoice/js/app.js"></script>

    <script src="Invoice/js/html2canvas.js"></script>
    <script src="Invoice/js/jspdf.min.js"></script>
    <title></title>

    <script>
        function downloadInvoice() {
            // Assuming 'jsPDF' library is included
            var doc = new jsPDF({
                orientation: 'portrait', // Ensure portrait mode
                format: 'a3'
            });

            // Generate content to be included in the PDF
            var invoiceContent = document.getElementById('invoice_wrapper');

            // Convert HTML to canvas then to PDF
            html2canvas(invoiceContent).then(function (canvas) {
                var imgData = canvas.toDataURL('image/png');
                doc.addImage(imgData, 'PNG', 0, 0, doc.internal.pageSize.width, doc.internal.pageSize.height);
                doc.save('invoice.pdf');
            });
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="invoice-6 invoice-content">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="invoice-inner clearfix">
                                <div class="invoice-info clearfix" id="invoice_wrapper">
                                    <div class="invoice-headar">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="invoice-logo">
                                                    <!-- Replace with dynamic logo -->
                                                    <img src="/img/Green Vintage Agriculture and Farming Logo.png" alt="logo" style="height:200px; width:200px">
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="invoice-contact-us">
                                                    <h1>Contact Us</h1>
                                                    <!-- Replace with dynamic contact details -->
                                                    <ul class="link">
                                                        <li>
                                                            <i class="fa fa-map-marker"></i> Kharghar, Navi Mumbai 410210
                                                        </li>
                                                        <li>
                                                            <i class="fa fa-envelope"></i> <a href="mailto:sahilharshalv@gmail.com">info@Fruitables.com</a>
                                                        </li>
                                                        <li>
                                                            <i class="fa fa-phone"></i> <a href="tel:+55-417-634-7071">+91 9022803784</a>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="invoice-contant">
                                        <div class="invoice-top">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <h1 class="invoice-name">Invoice</h1>
                                                </div>
                                                <div class="col-sm-6 mb-30">
                                                    <div class="invoice-number-inner">
                                                        <!-- Replace with dynamic invoice number and date -->
                                                        <h2 class="name">Invoice No: #<span id="InvoiceNumber" runat="server"></span></h2>
                                                        <p class="mb-0">Invoice Date: <span id="InvoiceDate" runat="server"></span></p>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 mb-30">
                                                    <div class="invoice-number">
                                                        <h4 class="inv-title-1">Invoice To</h4>
                                                        <!-- Replace with dynamic customer details -->
                                                        <h2 class="name mb-10"><span id="Username" runat="server"></span></h2>
                                                        <p class="invo-addr-1 mb-0" id="UserAddress" runat="server"></p>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 mb-30">
                                                    <div class="invoice-number">
                                                        <div class="invoice-number-inner">
                                                            <h4 class="inv-title-1">Invoice From</h4>
                                                            <!-- Replace with dynamic company details -->
                                                            <h2 class="name mb-10">Animas Roky</h2>
                                                            <p class="invo-addr-1 mb-0">
                                                                Apexo Inc  <br/>
                                                                billing@apexo.com <br/>
                                                                169 Teroghoria, Bangladesh <br/>
                                                            </p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="invoice-center">
                                            <div class="order-summary">
                                                <div class="table-outer">
                                                     <table class="default-table invoice-table">
                                                        <thead>
                                                            <tr>
                                                                <th>Description</th>
                                                                <th>Price</th>
                                                                <th>Quantity</th>
                                                                <th>Total</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="ProductsRepeater" runat="server">
    <ItemTemplate>
        <tr>
            <td><%# Eval("ProductName") %></td>
            <td>$<%# Convert.ToDecimal(Eval("Price")).ToString("0.00") %></td>
            <td><%# Convert.ToInt32(Eval("Quantity")) %></td>
            <td>$<%# (Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity"))).ToString("0.00") %></td>
        </tr>
    </ItemTemplate>
</asp:Repeater>
                                                            <tr>
                                                                <td><strong>Total Due</strong></td>
                                                                <td></td>
                                                                <td></td>
                                                                <td><strong>$<span id="TotalAmount" runat="server"></span></strong></td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="invoice-bottom">
                                                <div class="row">
                                                    <div class="col-lg-7 col-md-7 col-sm-7">
                                                        <div class="terms-conditions mb-30">
                                                            <h3 class="inv-title-1 mb-10">Terms & Conditions</h3>
                                                            Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy.
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-5 col-md-5 col-sm-5">
                                                        <div class="payment-method mb-30">
                                                            <h3 class="inv-title-1 mb-10">Payment Method</h3>
                                                            <!-- Replace with dynamic payment method details -->
                                                            <ul class="payment-method-list-1 text-14">
                                                                <li><strong>Account No:</strong> 00 123 647 840</li>
                                                                <li><strong>Account Name:</strong> Jhon Doe</li>
                                                                <li><strong>Branch Name:</strong> xyz</li>
                                                            </ul>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="invoice-btn-section clearfix d-print-none">
                                        <a href="javascript:window.print()" class="btn btn-lg btn-print">
                                            <i class="fa fa-print"></i> Print Invoice
                                        </a>
                                        <a href="javascript:void(0);" onclick="downloadInvoice()" class="btn btn-lg btn-download btn-theme">
    <i class="fa fa-download"></i> Download Invoice
</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Invoice 6 end -->
        </div>
    </form>
</body>
</html>

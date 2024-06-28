<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".nav-pills a").click(function (e) {
            e.preventDefault();
            var category = $(this).text().trim();
            $.ajax({
                type: "POST",
                url: "YourPage.aspx/GetProductsByCategory",
                data: JSON.stringify({ category: category }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var products = response.d;
                    var repeater = $("#<%= rptProducts.ClientID %>");
                    repeater.empty();
                    $.each(products, function (i, product) {
                        var item = `
                            <div class="col-md-6 col-lg-4 col-xl-3">
                                <div class="rounded position-relative product-item">
                                    <div class="product-img">
                                        <img src='${product.ImagePath}' class="img-fluid w-100 rounded-top" alt='${product.Name}' />
                                    </div>
                                    <div class="text-white bg-secondary px-3 py-1 rounded position-absolute" style="top: 10px; left: 10px;">${product.Category}</div>
                                    <div class="p-4 border border-secondary border-top-0 rounded-bottom">
                                        <h4>${product.Name}</h4>
                                        <p>${product.Description}</p>
                                        <div class="d-flex justify-content-between flex-lg-wrap">
                                            <p class="text-dark fs-5 fw-bold mb-0">$${product.Price} / kg</p>
                                            <a href="#" class="btn border border-secondary rounded-pill px-3 text-primary">
                                                <i class="fa fa-shopping-bag me-2 text-primary"></i> Add to cart
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>`;
                        repeater.append(item);
                    });
                }
            });
        });

        // Trigger click on the "All Products" tab to load default products
        $(".nav-pills a.active").click();
    });
</script>

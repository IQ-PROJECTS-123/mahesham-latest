<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authlogin.aspx.cs" Inherits="Maheshamv3.authlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>IQ-INDIA || Success is your way</title>

    <link rel="icon" type="image/x-icon" href="favicon.ico" />

    <!-- Library Stylesheets -->
    <link href="lib/animate/animate.min.css" rel="stylesheet" />
    <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="lib/lightbox/css/lightbox.min.css" rel="stylesheet" />

    <!-- Bootstrap & Custom Stylesheets -->
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/styles.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- Page Wrapper -->
        <div class="page-wrapper" id="main-wrapper" data-layout="vertical" data-navbarbg="skin6"
             data-sidebartype="full" data-sidebar-position="fixed" data-header-position="fixed">
            
            <div class="position-relative overflow-hidden radial-gradient min-vh-100 d-flex align-items-center justify-content-center">
                <div class="d-flex align-items-center justify-content-center w-100">
                    <div class="row justify-content-center w-100">
                        <div class="col-md-8 col-lg-6 col-xxl-3">
                            <div class="card mb-0">
                                <div class="card-body text-center">
                                    <!-- Logo -->
                                    <a href="./index.html" class="text-nowrap d-block py-3 w-100">
                                        <img src="img/logo.jpg" width="180" alt="IQ-India Logo" />
                                    </a>
                                    <p class="text-center mb-4">Your Way to Success</p>

                                    <!-- Email/Phone Input -->
                                    <div class="mb-3 text-start">
                                        <label for="_TextBoxUser" class="form-label">Email / Phone</label>
                                        <asp:TextBox runat="server" ID="_TextBoxUser" CssClass="form-control"
                                                     aria-describedby="emailHelp"></asp:TextBox>
                                    </div>

                                    <!-- Password Input -->
                                    <div class="mb-4 text-start">
                                        <label for="_TextBoxPWD" class="form-label">Password</label>
                                        <asp:TextBox runat="server" ID="_TextBoxPWD" TextMode="Password"
                                                     CssClass="form-control"></asp:TextBox>
                                    </div>

                                    <!-- Forgot Password Link -->
                                    <div class="mb-4 text-end">
                                        <a class="text-primary fw-bold" href="Forgetpassword.aspx">Forgot Password?</a>
                                    </div>

                                    <!-- Sign In Button -->
                                    <asp:Button ID="ButtonSign" runat="server" Text="Sign In" OnClick="ButtonSign_Click" CssClass="btn btn-primary w-100 py-2 fs-5 rounded-2" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Scripts -->
        <script src="assets/libs/jquery/dist/jquery.min.js"></script>
        <script src="assets/libs/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>

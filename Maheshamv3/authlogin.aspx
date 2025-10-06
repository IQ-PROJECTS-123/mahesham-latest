<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authlogin.aspx.cs" Inherits="Maheshamv3.authlogin" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>IQ-INDIA || Success is your way</title>
       
     <link rel="icon" type="image/x-icon" href="favicon.ico">
    <link rel="stylesheet" href="assets/css/styles.min.css" />
      <!-- Libraries Stylesheet -->
  <link href="lib/animate/animate.min.css" rel="stylesheet">
  <link href="lib/owlcarousel/assets/owl.carousel.min.css" rel="stylesheet">
  <link href="lib/lightbox/css/lightbox.min.css" rel="stylesheet">

   
   <link rel="icon" type="image/x-icon" href="favicon.ico">
  <!-- Customized Bootstrap Stylesheet -->
  <link href="css/bootstrap.min.css" rel="stylesheet">

  <!-- Template Stylesheet -->
  <link href="css/style.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <!--  Body Wrapper -->
        <div class="page-wrapper" id="main-wrapper" data-layout="vertical" data-navbarbg="skin6" data-sidebartype="full"
            data-sidebar-position="fixed" data-header-position="fixed">
            <div
                class="position-relative overflow-hidden radial-gradient min-vh-100 d-flex align-items-center justify-content-center">
                <div class="d-flex align-items-center justify-content-center w-100">
                    <div class="row justify-content-center w-100">
                        <div class="col-md-8 col-lg-6 col-xxl-3">
                            <div class="card mb-0">
                                <div class="card-body">
                                    <a href="./index.html" class="text-nowrap logo-img text-center d-block py-3 w-100">
                                        <img src="img/logo.jpg" width="180" alt="" />
                                    </a>
                                    <p class="text-center">Your Way to Success</p>
                                    <%--<div class="mb-3">
                                        <label for="exampleInputEmail1" class="form-label">Login As</label>
                                        <asp:DropDownList runat="server" ID="_DropDownListType" CssClass="form-control">
                                            <asp:ListItem Value="S" Text="Student"></asp:ListItem>
                                            <asp:ListItem Value="M" Text="Mentor or Staff"></asp:ListItem>
                                            <asp:ListItem Value="A" Text="Alumni"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>--%>
                                    <div class="mb-3">
                                        <label for="exampleInputEmail1" class="form-label">Email\Phone</label>
                                        <%-- <input type="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">--%>
                                        <asp:TextBox runat="server" ID="_TextBoxUser" CssClass="form-control" aria-describedby="emailHelp"></asp:TextBox>
                                    </div>
                                    <div class="mb-4">
                                        <label for="exampleInputPassword1" class="form-label">Password</label>
                                        <%-- <input type="password" class="form-control" id="exampleInputPassword1">--%>
                                        <asp:TextBox runat="server" ID="_TextBoxPWD" type="email" CssClass="form-control" TextMode="Password"></asp:TextBox>

                                    </div>
                                   <div class="d-flex align-items-center justify-content-between mb-4" style="display:none">
                                        <div class="form-check">
                                             <%--  <asp:CheckBox runat="server" ID="_CheckBoxMentor" class="form-check-input primary" />
                                            <label class="form-check-label text-dark" for="flexCheckChecked">
                                                Login as Mentor
                                            </label>--%>
                                             <a class="text-primary fw-bold" href="authkey.aspx">Forgot Password ?</a>
                                        </div>
                                      
                                    </div>
                                    <asp:Button ID="ButtonSign" OnClick="ButtonSign_Click" runat="server" Text="Sign In" CssClass="btn btn-primary w-100 py-8 fs-4 mb-4 rounded-2" />

                                    <%-- <div class="d-flex align-items-center justify-content-center">
                    <p class="fs-4 mb-0 fw-bold">New to Modernize?</p>
                    <a class="text-primary fw-bold ms-2" href="./authentication-register.html">Create an account</a>
                  </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="assets/libs/jquery/dist/jquery.min.js"></script>
        <script src="assets/libs/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    </form>
</body>
</html>

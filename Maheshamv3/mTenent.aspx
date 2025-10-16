<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="mTenent.aspx.cs" Inherits="Maheshamv3.mTenent" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid contact py-5">
        <div class="container py-5">
            <div class="pb-5">
                <div class="bg-light rounded p-4 pb-0">
                    <div class="row g-5 align-items-center">
                        <div class="col-lg-12 wow fadeInLeft" data-wow-delay="0.1s">
                            <h2 class="display-5 mb-2"><a href="Dashboard.aspx">Dashboard</a> - Tenent</h2>
                            <div class="row g-3">
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:DropDownList runat="server" ID="_DropDownListType" AutoPostBack="true"
                                            OnSelectedIndexChanged="_DropDownListFacility_SelectedIndexChanged"
                                            class="form-control" ClientIDMode="Static">
                                            <asp:ListItem Text="Main Tenant"></asp:ListItem>
                                            <asp:ListItem Text="Partner Tenant"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="_DropDownListType">Tenant Type</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:DropDownList runat="server" ID="_DropDownListFacility" AutoPostBack="true" OnSelectedIndexChanged="_DropDownListFacility_SelectedIndexChanged" class="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="">-- Select Room --</asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="_DropDownListFacility">ROOM</label>
                                    </div>
                                    <!-- Validation -->
                                    <asp:RequiredFieldValidator ID="rfvRoom" runat="server" ControlToValidate="_DropDownListFacility" InitialValue="" ErrorMessage="Please select a room!" Display="None"
                                        ValidationGroup="TenantForm">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextName" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextName">Your Name</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxAmount" TextMode="Number" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxAmount">Your Monthly Rental</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextAdvPayment" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextAdvPayment">Advance Payment</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxStartDate" TextMode="Date" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxStartDate">Your Rent Start Date</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxMeter" TextMode="Number" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxMeter">Meter Reading Start</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxEmail" TextMode="Email" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxEmail">Your Email</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating position-relative">
                                        <asp:TextBox runat="server" ID="_TextBoxPWD" CssClass="form-control pwd-field"
                                            TextMode="Password" ClientIDMode="Static" placeholder="Password"></asp:TextBox>
                                        <span class="position-absolute top-50 end-0 translate-middle-y pe-3" style="cursor: pointer;" id="togglePwd">
                                            <i class="bi bi-eye" id="iconPwd"></i>
                                        </span>
                                        <label for="_TextBoxPWD">Password</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxAadhar" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxAadhar">Your Aadhar</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxPAN" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxPAN">Your PAN</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxVoter" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxVoter">Your Voter ID</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxMobile1" MaxLength="10" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxMobile1">Your Primary Mobile</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxMobile2" MaxLength="10" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxMobile2">Your Secondary Mobile</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxFather" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxFather">Father's Name</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxFContact" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxMobile2">Father's Contact</label>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxAddress" required placeholder="Leave a address here" class="form-control" TextMode="MultiLine" Rows="4" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxAddress">Address</label>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Literal runat="server" ID="Literal1"></asp:Literal>
                                    <div class="col-12">
                                        <asp:Literal runat="server" ID="_LiteralMSG"></asp:Literal>
                                        <!-- Submit & KYC buttons stay together -->
                                        <div class="mb-3">
                                            <asp:Button runat="server" ID="_ButtonSubmit" Text="Submit" CssClass="btn btn-primary w-100 py-3" OnClick="_ButtonSubmit_Click" ValidationGroup="TenantForm" />
                                            <asp:Button runat="server" ID="_ButtonDocVeri" Text="KYC Document.." CssClass="btn btn-danger w-100 py-3 mt-2" OnClick="_ButtonDocVeri_Click" Visible="false" />
                                            <asp:Button runat="server" ID="_ButtonAddMore" Text="Add More.." CssClass="btn btn-success w-100 py-3 mt-2" Visible="false" OnClick="_ButtonAddMore_Click" />
                                        </div>
                                        <!-- Popup Alert for Validation -->
                                        <asp:ValidationSummary ID="vsPopup" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="TenantForm" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="Maheshamv3.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid contact py-5">
        <div class="container py-5">
            <div class="pb-5">
                <div class="bg-light rounded p-4 pb-0">
                    <h2 class="display-5 mb-3">
                        <a href="Dashboard.aspx">Dashboard</a> - Payment
                    </h2>
                    <div class="row g-5 align-items-center">
                        <div class="col-lg-12 wow fadeInLeft" data-wow-delay="0.1s" style="visibility: visible; animation-delay: 0.1s; animation-name: fadeInLeft;">
                            <div class="row g-3">
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:Label runat="server" ID="_LabelMonth" class="form-control" ClientIDMode="Static"></asp:Label>
                                        <label for="_LabelMonth">Rent Month</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:Label runat="server" ID="_LabelName" class="form-control" ClientIDMode="Static"></asp:Label>
                                        <label for="_LabelName">Name</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:Label runat="server" ID="_LabelRoom" class="form-control" ClientIDMode="Static"></asp:Label>
                                        <label for="_LabelRoom">ROOM</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:Label runat="server" ID="_LabelTotal" class="form-control" ClientIDMode="Static"></asp:Label>
                                        <label for="_LabelTotal">Total</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxAmount" TextMode="Number" required class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxReading">Paid Amount</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:DropDownList runat="server" ID="_DropDownListType" class="form-control" ClientIDMode="Static">
                                            <asp:ListItem Text="Scan"></asp:ListItem>
                                            <asp:ListItem Text="UPI"></asp:ListItem>
                                            <asp:ListItem Text="Bank"></asp:ListItem>
                                            <asp:ListItem Text="Cash"></asp:ListItem>
                                        </asp:DropDownList>
                                        <label for="_DropDownListType">Payment Type</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:FileUpload runat="server" ID="_FileUpload" class="form-control" ClientIDMode="Static"></asp:FileUpload>
                                        <label for="_FileUpload">Screen Capture</label>
                                    </div>
                                </div>
                                <div class="col-lg-12 col-xl-6">
                                    <div class="form-floating">
                                        <asp:TextBox runat="server" ID="_TextBoxNote" class="form-control" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                        <label for="_TextBoxNote">Note</label>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <asp:Literal runat="server" ID="_LiteralMSG"></asp:Literal>
                                    <asp:Button runat="server" Visible="false" ID="_ButtonSubmit" Text="Submit" CssClass="btn btn-primary w-100 py-3" OnClick="_ButtonSubmit_Click" />
                                    <asp:Button runat="server" Visible="false" OnClientClick="window.close(); return false;" ID="_ButtonNewReading" Text="Close" CssClass="btn btn-primary w-100 py-3" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

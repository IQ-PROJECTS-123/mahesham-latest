<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rental.aspx.cs" Inherits="Maheshamv3.Rental" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid contact py-5">
        <div class="container py-5">
            <h2 class="display-5 mb-2"><a href="Dashboard.aspx">Dashboard</a> - Rental</h2>

            <div class="row g-3 mb-3">
                <div class="col-lg-6">
                    <div class="form-floating">
                        <asp:DropDownList ID="_DropDownListYear" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="_DropDownListYear_SelectedIndexChanged"
                            class="form-control">
                            <asp:ListItem Text="2024" />
                            <asp:ListItem Text="2025" />
                            <asp:ListItem Text="2026" />
                            <asp:ListItem Text="2027" />
                            <asp:ListItem Text="2028" />
                        </asp:DropDownList>
                        <label for="_DropDownListYear">Year</label>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="form-floating">
                        <asp:DropDownList ID="_DropDownListMonth" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="_DropDownListMonth_SelectedIndexChanged"
                            class="form-control">
                            <asp:ListItem Value="JAN" Text="JAN" />
                            <asp:ListItem Value="FEB" Text="FEB" />
                            <asp:ListItem Value="MAR" Text="MAR" />
                            <asp:ListItem Value="APR" Text="APR" />
                            <asp:ListItem Value="MAY" Text="MAY" />
                            <asp:ListItem Value="JUN" Text="JUN" />
                            <asp:ListItem Value="JUL" Text="JUL" />
                            <asp:ListItem Value="AUG" Text="AUG" />
                            <asp:ListItem Value="SEP" Text="SEP" />
                            <asp:ListItem Value="OCT" Text="OCT" />
                            <asp:ListItem Value="NOV" Text="NOV" />
                            <asp:ListItem Value="DEC" Text="DEC" />
                        </asp:DropDownList>
                        <label for="_DropDownListMonth">Month</label>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="form-floating">
                        <asp:DropDownList ID="_dropdownloadRent" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="_dropdownloadRent_SelectedIndexChanged"
                            class="form-control">
                            <asp:ListItem Text="All" />
                            <asp:ListItem Text="Rental" />
                            <asp:ListItem Text="Advance" />
                            <asp:ListItem Text="Vacent" />
                        </asp:DropDownList>
                        <label for="_dropdownloadRent">Rent Type</label>
                    </div>
                </div>
            </div>
            <asp:Panel ID="PanelAdvance" runat="server">
                <h1 class="h4 fw-bold text-primary bg-light border-start border-5 border-primary px-3 py-2 rounded shadow-sm mb-3">Advance
                </h1>
                <asp:GridView ID="_GridViewAdvance" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="SL. No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo">
                            <ItemTemplate>
                                <img src='/assets/Photo/abc.jpg' width="35" height="35" class="rounded-circle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="facility" HeaderText="ROOM" />
                        <asp:BoundField DataField="Mobile1" HeaderText="Mobile" />
                        <asp:BoundField DataField="PeriodStart" HeaderText="Period Start" />
                        <asp:BoundField DataField="PeriodEnd" HeaderText="Period End" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="MeterStart" HeaderText="Meter Start" />
                        <asp:BoundField DataField="MeterEnd" HeaderText="Meter End" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" />
                        <asp:BoundField DataField="Bill" HeaderText="Bill" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="_ImageButtonView" runat="server" ImageUrl="~/img/edit.png"
                                    CommandArgument='<%# Bind("ID") %>' Width="30px"
                                    OnClick="_ImageButtonView_Click" ToolTip="View Payment Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <!-- ===== Total Amount Label ===== -->
                <div class="row mb-4">
                    <div class="col-lg-12">
                        <div class="card shadow-sm border-success">
                            <div class="card-body d-flex justify-content-between align-items-center">
                                <span class="h5 mb-0 text-success fw-bold">Total Advance Amount:</span>
                                <asp:Label ID="lblTotalAdvance" runat="server" CssClass="h5 mb-0 text-success fw-bold"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="PanelRent" runat="server">
                <h1 class="h4 fw-bold text-primary bg-light border-start border-5 border-primary px-3 py-2 rounded shadow-sm mb-3">Rent
                </h1>
                <asp:GridView ID="_GridViewRent" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="SL. No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo">
                            <ItemTemplate>
                                <img src='/assets/Photo/abc.jpg' width="35" height="35" class="rounded-circle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="facility" HeaderText="ROOM" />
                        <asp:BoundField DataField="Mobile1" HeaderText="Mobile" />
                        <asp:BoundField DataField="PeriodStart" HeaderText="Period Start" />
                        <asp:BoundField DataField="PeriodEnd" HeaderText="Period End" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="MeterStart" HeaderText="Meter Start" />
                        <asp:BoundField DataField="MeterEnd" HeaderText="Meter End" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" />
                        <asp:BoundField DataField="Bill" HeaderText="Bill" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="_ImageButtonView" runat="server" ImageUrl="~/img/edit.png"
                                    CommandArgument='<%# Bind("ID") %>' Width="30px"
                                    OnClick="_ImageButtonView_Click" ToolTip="View Payment Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <!-- ===== Total Amount Label ===== -->
                <div class="row mb-4">
                    <div class="col-lg-12">
                        <div class="card shadow-sm border-success">
                            <div class="card-body d-flex justify-content-between align-items-center">
                                <span class="h5 mb-0 text-success fw-bold">Total Paid Amount:</span>
                                <asp:Label ID="lblTotalPaid" runat="server" CssClass="h5 mb-0 text-success fw-bold"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
         <%--   <asp:Panel ID="PanelEmpty" runat="server">
                <h1 class="h4 fw-bold text-primary bg-light border-start border-5 border-primary px-3 py-2 rounded shadow-sm mb-3">Empty
                </h1>
                <asp:GridView ID="_GridViewEmpty" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="SL. No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo">
                            <ItemTemplate>
                                <img src='/assets/Photo/abc.jpg' width="35" height="35" class="rounded-circle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="facility" HeaderText="ROOM" />
                        <asp:BoundField DataField="Mobile1" HeaderText="Mobile" />
                        <asp:BoundField DataField="PeriodStart" HeaderText="Period Start" />
                        <asp:BoundField DataField="PeriodEnd" HeaderText="Period End" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="MeterStart" HeaderText="Meter Start" />
                        <asp:BoundField DataField="MeterEnd" HeaderText="Meter End" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" />
                        <asp:BoundField DataField="Bill" HeaderText="Bill" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="_ImageButtonView" runat="server" ImageUrl="~/img/edit.png"
                                    CommandArgument='<%# Bind("ID") %>' Width="30px"
                                    OnClick="_ImageButtonView_Click" ToolTip="View Payment Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>--%>
            <asp:Panel ID="PanelVacent" runat="server">
                <h1 class="h4 fw-bold text-primary bg-light border-start border-5 border-primary px-3 py-2 rounded shadow-sm mb-3">Vacent
                </h1>
                <asp:GridView ID="_GridViewVacent" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-bordered table-hover">
                    <Columns>
                        <asp:TemplateField HeaderText="SL. No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Photo">
                            <ItemTemplate>
                                <img src='/assets/Photo/abc.jpg' width="35" height="35" class="rounded-circle" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="facility" HeaderText="ROOM" />
                        <asp:BoundField DataField="Mobile1" HeaderText="Mobile" />
                        <asp:BoundField DataField="PeriodStart" HeaderText="Period Start" />
                        <asp:BoundField DataField="PeriodEnd" HeaderText="Period End" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
                        <asp:BoundField DataField="MeterStart" HeaderText="Meter Start" />
                        <asp:BoundField DataField="MeterEnd" HeaderText="Meter End" />
                        <asp:BoundField DataField="Unit" HeaderText="Unit" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" />
                        <asp:BoundField DataField="Bill" HeaderText="Bill" />
                        <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                        <asp:BoundField DataField="PaidAmount" HeaderText="Paid" />
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="_ImageButtonView" runat="server" ImageUrl="~/img/edit.png"
                                    CommandArgument='<%# Bind("ID") %>' Width="30px"
                                    OnClick="_ImageButtonView_Click1" ToolTip="View Payment Details" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>

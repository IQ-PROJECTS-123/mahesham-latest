<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tenent.aspx.cs" Inherits="Maheshamv3.Tenent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid contact py-5">
        <div class="container py-5">
            <div class="pb-5">
                <div class="bg-light rounded p-4 pb-0">
                    <div class="row g-5 align-items-center">
                        <div class="col-lg-12 wow fadeInLeft" data-wow-delay="0.1s" style="visibility: visible; animation-delay: 0.1s; animation-name: fadeInLeft;">
                            <h2 class="display-5 mb-2"> <a href="Dashboard.aspx" >Dashboard</a> - Tenent Master</h2>
                            <asp:GridView ID="GridView2" class="table table-bordered table-condensed table-responsive table-hover " runat="server" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL. No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Photo">
                                        <ItemTemplate>
                                            <img src='/assets/Photo/abc.jpg' width="35" height="35" class="rounded-circle" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" />
                                    <asp:BoundField DataField="facility" HeaderText="ROOM" />
                                    <asp:BoundField DataField="StartOn" HeaderText="Start On" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" />
                                    <asp:BoundField DataField="Advance" HeaderText="Advance Payment" />
                                    <asp:BoundField DataField="Mobile1" HeaderText="Mobile" />
                                    <asp:BoundField DataField="Mobile2" HeaderText="Mobile2" />
                                    <asp:BoundField DataField="AadharNumber" HeaderText="Aadhar" />
                                    <asp:BoundField DataField="PANNumber" HeaderText="PAN" />
                                    <asp:BoundField DataField="VoterNumber" HeaderText="VoterID" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:ImageButton CommandArgument='<%# Bind("ID") %>' ID="_ImageButtonView" runat="server" ImageUrl="~/img/edit.png" Width="30px" ToolTip="View Payment Details" OnClientClick="target ='_blank';" OnClick="_ImageButtonView_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

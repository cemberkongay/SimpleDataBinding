<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DataBinding.aspx.cs" Inherits="SimpleDataBinding.DataBinding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1>Data Binding Example</h1>
        <h4>
            <asp:Literal ID="ltError" runat="server" ></asp:Literal>
        </h4>
        <asp:GridView ID="gvColor" CssClass="table table-striped" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvColor_RowDeleting" OnRowEditing="gvColor_RowEditing" OnRowUpdating="gvColor_RowUpdating" OnRowCancelingEdit="gvColor_RowCancelingEdit">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnColorId" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "ColorID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="HexCode" HeaderText="Hex Code" />
                <asp:TemplateField HeaderText="Color Swatch">
                    <ItemTemplate>
                        <div class="color-swatch" >
                            <div class="color-swatch" style='<%# "background-color:#" + Eval("HexCode") + ";"  %>' >
                                <label></label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="true" />
                <asp:CommandField ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        <div class="row color-table">
            <asp:Button ID="btnAddRow" runat="server" Text="Add New Row" CssClass="btn btn-primary" OnClick="btnAddRow_Click" />
        </div>
</asp:Content>

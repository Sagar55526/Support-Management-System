<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSupportDash.aspx.cs" Inherits="SupportMngmntSystem.AdminSupport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- bootstrap css --%>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <%-- datatable css --%>
    <link href="Datatable\css\jquery.dataTables.min.css" rel="stylesheet" />
    <%-- bootstrap js --%>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-5">

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dev_SchoolMasterConnectionString %>" SelectCommand="SELECT [UserID], [Query], [Status], [time], [QueryID], [SchoolID] FROM [SupportMaster]"></asp:SqlDataSource>
            <asp:GridView class="table is-striped table-bordered table-condensed gridview2 hover cell-border stripe ui celled table" ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:BoundField DataField="QueryID" HeaderText="QueryID" InsertVisible="False" ReadOnly="True" SortExpression="QueryID" />
                    <asp:BoundField DataField="Query" HeaderText="Query" SortExpression="Query" />
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                    <asp:BoundField DataField="time" HeaderText="time" SortExpression="time" />
                    <asp:templatefield headertext="View " headerstyle-cssclass="gridheader">
                        <itemtemplate>
                            <asp:Button ID="btnview" runat="server" Text="View" CssClass="btn btn-block btn-outline-info"
                                        PostBackUrl='<%# "~/QueryReply.aspx?QueryID=" + Eval("QueryID") %>' />

                        </itemtemplate>
                    </asp:templatefield>
                </Columns>
            </asp:GridView>


        </div>
    </form>
</body>
</html>

 <%--PostBackUrl='<%# "~/QueryResult.aspx?QueryID=" + Eval("QueryID") %>' --%>

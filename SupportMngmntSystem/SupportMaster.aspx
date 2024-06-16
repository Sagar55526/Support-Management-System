<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupportMaster.aspx.cs" Inherits="SupportMngmntSystem.SupportMaster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- bootstrap css -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <!-- datatable css -->
    <link href="Datatable/css/jquery.dataTables.min.css" rel="stylesheet" />
    <!-- bootstrap js -->
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.14.7/dist/umd/popper.min.js" integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4xF86dIHNDz0W1" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.3.1/dist/js/bootstrap.min.js" integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-5">
            <div class="card text-bg-dark mb-3">
                <div class="card-header">
                    <div class="row">
                        <h5>Support Management System</h5>
                        <div class="col d-flex justify-content-end">
                            <div class="col-md-1">
                                <asp:Button CssClass="btn btn-outline-success btn-block" ID="btnNewQuery" runat="server" Text="New" OnClick="btnNewQuery_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>

                    <div class="row">
                        <div class="col-md-4">
                            <asp:Label ID="lblUserName" runat="server" Text="युजरचे नाव :"></asp:Label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="txtUserName" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <asp:Label ID="lblSchoolName" runat="server" Text="शाळेचे नाव :"></asp:Label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="txtSchoolName" runat="server" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:Label ID="lblQuery" runat="server" Text="आपली तक्रार/अडचण :"></asp:Label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="txtQuery" runat="server" ReadOnly="False" TextMode="MultiLine" Style="height: 150px;"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <asp:Label ID="lblReply" runat="server" Text="प्रतिसाद :"></asp:Label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="txtReply" runat="server" ReadOnly="False" TextMode="MultiLine" Style="height: 150px;" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblStatus" runat="server" Text="तक्रारिची स्थिती :"></asp:Label>
                            <div class="form-group">
                                <asp:TextBox class="form-control" ID="txtStatus" runat="server" ReadOnly="True">Pending</asp:TextBox>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col d-flex justify-content-end">
                            <div class="col-md-1"> 
                                <asp:Button CssClass="btn btn-outline-info btn-block" ID="QuerySubmitBtn" runat="server" Text="Submit" OnClick="QuerySubmitBtn_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid mt-5">
            <div class="row">
                <div class="col">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Dev_SchoolMasterConnectionString %>" SelectCommand="SELECT [QueryID], [Status], [time], [Query], [QueryReply], [ReplyDate] FROM [SupportMaster]"></asp:SqlDataSource>
                    <asp:GridView class="table table-striped table-bordered gridview2 hover cell-border stripe ui celled table" ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Query" HeaderText="Query" SortExpression="Query" />
                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                            <asp:BoundField DataField="time" HeaderText="Date" SortExpression="time" />
                            <asp:BoundField DataField="QueryReply" HeaderText="QueryReply" SortExpression="QueryReply" />
                            <asp:BoundField DataField="ReplyDate" HeaderText="ReplyDate" SortExpression="ReplyDate" />
                            <asp:TemplateField HeaderText="View " HeaderStyle-CssClass="gridheader">
                                <ItemTemplate>
                                    <asp:Button ID="btnview" runat="server" Text="View" CssClass="btn btn-block btn-outline-info" CommandName="View" CommandArgument='<%# Eval("QueryID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

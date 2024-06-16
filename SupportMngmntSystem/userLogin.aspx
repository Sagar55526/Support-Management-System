<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userLogin.aspx.cs" Inherits="SupportMngmntSystem.userLogin" %>

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
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6 mb-3 mx-auto">
                    <div class="card">
                        <div class="card-header" style="font-size: larger;">
                            Log-in
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col">
                                    <center>
                                        <i class="fa-solid fa-user fa-5x mt-2"></i>
                                    </center>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="Label1" runat="server" Text="User-ID"></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" ID="txtUserName" placeHolder="Enter UserName" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                                    <div class="form-group">
                                        <asp:TextBox class="form-control" ID="txtPassword" placeHolder="Enter password" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col">
                                    <asp:Button CssClass="btn btn-outline-success fa-pull-right" ID="loginBtn" runat="server" Text="Log-in" OnClick="btnLogin_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

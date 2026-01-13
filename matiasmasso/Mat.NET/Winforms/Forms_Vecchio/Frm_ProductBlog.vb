

Public Class Frm_ProductBlog

    Private mProductBlog As ProductBlog
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oProductBlog As ProductBlog)
        MyBase.new()
        Me.InitializeComponent()
        mProductBlog = oProductBlog
        'Me.Text = mObject.ToString
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mProductBlog
            DateTimePicker1.Value = .Fch
            TextBoxBlogger.Text = .Blogger
            TextBoxTitle.Text = .Title
            TextBoxExtracte.Text = .Extracte
            TextBoxUrl.Text = .Url
            Xl_Product1.Product = .Product

            If .Exists Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxBlogger.TextChanged, _
         TextBoxTitle.TextChanged, _
         TextBoxExtracte.TextChanged, _
          TextBoxUrl.TextChanged, _
           DateTimePicker1.ValueChanged, _
            Xl_Product1.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mProductBlog
            .Blogger = TextBoxBlogger.Text
            .Title = TextBoxTitle.Text
            .Extracte = TextBoxExtracte.Text
            .Url = TextBoxUrl.Text
            .Fch = DateTimePicker1.Value
            .Product = Xl_Product1.Product
            .Update()
            RaiseEvent AfterUpdate(mProductBlog, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mProductBlog.AllowDelete Then
            mProductBlog.Delete()
            Me.Close()
        End If
    End Sub
End Class

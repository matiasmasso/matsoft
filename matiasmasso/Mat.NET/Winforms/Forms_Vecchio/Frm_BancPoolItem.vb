

Public Class Frm_BancPoolItem

    Private mBancPool As BancPool
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oBancPool As BancPool)
        MyBase.new()
        Me.InitializeComponent()
        mBancPool = oBancPool
        'Me.Text = mBancPool.ToString
        LoadProducts()
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With mBancPool
            Xl_Contact_Logo1.Contact = .Banc
            ComboBoxProductCategories.SelectedItem = ComboBoxProductCategories.Items(CInt(.ProductCategory))
            DateTimePickerFchFrom.Value = .FchFrom

            If .FchTo = Date.MinValue Then
                CheckBoxFchTo.Checked = False
                DateTimePickerFchTo.Visible = False
            Else
                CheckBoxFchTo.Checked = True
                DateTimePickerFchTo.Visible = True
                DateTimePickerFchTo.Value = .FchTo
            End If

            Xl_AmtCur1.Amt = .Amt

            If .Exists Then
                ButtonDel.Enabled = True
            End If
        End With
    End Sub

    Private Sub LoadProducts()
        ComboBoxProductCategories.Items.Clear()
        For Each s As String In [Enum].GetNames(GetType(BancPool.ProductCategories))
            ComboBoxProductCategories.Items.Add(s)
        Next
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        DateTimePickerFchFrom.ValueChanged, _
         DateTimePickerFchTo.ValueChanged, _
          ComboBoxProductCategories.SelectedIndexChanged, _
           Xl_AmtCur1.AfterUpdate

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If mAllowEvents Then
            DateTimePickerFchTo.Visible = CheckBoxFchTo.Checked
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mBancPool
            .FchFrom = DateTimePickerFchFrom.Value
            If CheckBoxFchTo.Checked Then
                .FchTo = DateTimePickerFchTo.Value
            Else
                .FchTo = Date.MinValue
            End If
            .ProductCategory = ComboBoxProductCategories.SelectedIndex
            .Amt = Xl_AmtCur1.Amt
            .Update()

            RaiseEvent AfterUpdate(mBancPool, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        mBancPool.Delete()
        Me.Close()
    End Sub


End Class
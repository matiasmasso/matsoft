Public Class Irpf

    Shared Function Factory(oEmp As DTOEmp, year As Integer, month As Integer) As DTOIrpf
        Dim retval = DTOIrpf.Factory(oEmp, year, month)
        IrpfLoader.LoadItems(retval)
        IrpfLoader.LoadSaldos(retval)
        Return retval
    End Function
End Class

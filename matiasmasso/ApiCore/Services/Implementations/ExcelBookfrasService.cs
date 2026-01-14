using Api.Entities;
using Api.Services.Interfaces;
using ClosedXML.Excel;
using ClosedXML.Excel.Drawings;
using DTO;
using System;

namespace Api.Services.Implementations
{
    public class ExcelBookfrasService : IExcelBookfrasService
    {
        private readonly IExcelBuilderService _excel;

        public ExcelBookfrasService(IExcelBuilderService excel)
        {
            _excel = excel;
        }

        public byte[] Excel(EmpModel.EmpIds emp, int year, string apiBaseUrl)
        {
            var facturas = BookfrasService.GetValues((int)emp, year);
            
            var wb = _excel.CreateWorkbook();
            var ws = _excel.AddSheet(wb, $"Factures rebudes {emp.ToString()} {year}");

            var headersRow = 1;
            var totalsRow = 2;
            int row = 3;


            // Cabecera
            _excel.AddHeaderRow(ws, headersRow,
                "Fecha", "Factura", "Proveedor", "Nif"
            );

            // Tipos de IVA dinámicos
            var tiposIva = facturas
                .SelectMany(f => f.Ivas.Select(i => i.Tipo ?? 0))
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            int col = 5;
            var columnas = new Dictionary<decimal, (int baseCol, int cuotaCol)>();


            foreach (var tipo in tiposIva)
            {
                columnas[(decimal)tipo] = (col, col + 1);

                var title = (tipo == 0 ? "base exempta" : $"Base {tipo}%");
                ws.Cell(headersRow, col).Value = title;
                //_excel.ApplyHeaderStyle(ws.Cell(2, col));

                ws.Cell(headersRow, col + 1).Value = $"Cuota {tipo}%";
                //_excel.ApplyHeaderStyle(ws.Cell(2, col + 1));

                col += 2;
            }

            int colTotalBase = col++;
            int colTotalCuota = col++;

            ws.Cell(headersRow, colTotalBase).Value = "Total Bases";
            //_excel.ApplyHeaderStyle(ws.Cell(2, colTotalBase));

            ws.Cell(headersRow, colTotalCuota).Value = "Total Cuotas";
            //_excel.ApplyHeaderStyle(ws.Cell(2, colTotalCuota));


            //ws.Row(row).Height = 20;   // enough for a 16×16 icon
            //ws.Column(4).Width = 5;    // narrow but visible


            // Datos
            row = 3;

            foreach (var f in facturas)
            {
                ws.Cell(row, 1).Value = ((DateOnly)f.FraFch!).ToDateTime(TimeOnly.MinValue);
                _excel.ApplyDateStyle(ws.Cell(row, 1));

                ws.Cell(row, 2).Value = f.FraNum;
                ws.Cell(row, 2).SetHyperlink(new XLHyperlink($"{apiBaseUrl}/cca/pdf/{f.CcaGuid.ToString()}"));

                ws.Cell(row, 3).Value = f.RaoSocial;
                ws.Cell(row, 4).Value = f.NIF;

                var items = f.Ivas
                    .GroupBy(i => i.Tipo ?? 0)
                    .ToDictionary(g => g.Key, g => new
                    {
                        Base = g.Sum(x => x.Base ?? 0),
                        Cuota = g.Sum(x => x.Quota ?? 0)
                    });

                foreach (var tipo in tiposIva)
                {
                    var (baseCol, cuotaCol) = columnas[(decimal)tipo!];

                    if (items.TryGetValue(tipo, out var datos))
                    {
                        ws.Cell(row, baseCol).Value = datos.Base;
                        _excel.ApplyCurrencyStyle(ws.Cell(row, baseCol));

                        ws.Cell(row, cuotaCol).Value = datos.Cuota;
                        _excel.ApplyCurrencyStyle(ws.Cell(row, cuotaCol));
                    }
                }

                // Totales por factura (solo bases)
                var baseCells = columnas.Values
                    .Select(v => ws.Cell(row, v.baseCol).Address.ToString())
                    .ToList();

                ws.Cell(row, colTotalBase).FormulaA1 =
                    $"SUM({string.Join(",", baseCells)})";
                _excel.ApplyCurrencyStyle(ws.Cell(row, colTotalBase));

                // Totales por factura (solo cuotas)
                var cuotaCells = columnas.Values
                    .Select(v => ws.Cell(row, v.cuotaCol).Address.ToString())
                    .ToList();

                ws.Cell(row, colTotalCuota).FormulaA1 =
                    $"SUM({string.Join(",", cuotaCells)})";
                _excel.ApplyCurrencyStyle(ws.Cell(row, colTotalCuota));

                row++;
            }

            // Totales globales
            for (int c = 5; c <= colTotalCuota; c++)
            {
                ws.Cell(totalsRow, c).FormulaA1 = $"SUM({ws.Cell(3, c).Address}:{ws.Cell(row - 1, c).Address})";
                _excel.ApplyCurrencyStyle(ws.Cell(totalsRow, c));
            }

            _excel.AutoAdjust(ws);

            return _excel.Save(wb);
        }

    }
}

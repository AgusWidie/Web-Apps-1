﻿using APIRetail.CacheList;
using APIRetail.Helper;
using APIRetail.Models.Database;
using APIRetail.Models.DTO.Request;
using APIRetail.Models.DTO.Response;
using APIRetail.Repository.IRepository;
using APIRetail.Services.Interface;

namespace APIRetail.Services
{
    public class StockOpnameService : IStockOpnameService
    {
        public readonly IConfiguration _configuration;
        public readonly retail_systemContext _context;
        public readonly ILogError _logError;
        public readonly IStockOpname _stockOpnameRepo;
        public readonly IDailyStock _dailyStockRepo;
        public readonly IMonthlyStock _monthlyStockRepo;

        public StockOpnameService(IConfiguration Configuration, retail_systemContext context, ILogError logError, IStockOpname stockOpnameRepo, IDailyStock dailyStockRepo, IMonthlyStock monthlyStockRepo)
        {
            _configuration = Configuration;
            _context = context;
            _logError = logError;
            _stockOpnameRepo = stockOpnameRepo;
            _dailyStockRepo = dailyStockRepo;
            _monthlyStockRepo = monthlyStockRepo;
        }

        public async Task<List<StockOpnameResponse>> GetStockOpname(StockOpnameRequest param, CancellationToken cancellationToken)
        {

            try
            {

                GarbageCollector.GarbageCollection();
                if (GeneralList._listStockOpname.Count() > 0)
                {
                    if (param.ProductTypeId != null && param.ProductTypeId != 0)
                    {
                        long Page = param.Page - 1;
                        var resultList = GeneralList._listStockOpname.Where(x => x.CompanyId == param.CompanyId && x.BranchId == param.BranchId && x.ProductTypeId == param.ProductTypeId);
                        var TotalPageSize = Math.Ceiling((decimal)resultList.Count() / (int)param.PageSize);
                        param.TotalPageSize = (long)TotalPageSize;
                        return resultList.Skip((int)Page * (int)param.PageSize).Take((int)param.PageSize).ToList();

                    }
                    else
                    {
                        long Page = param.Page - 1;
                        var resultList = GeneralList._listStockOpname.Where(x => x.CompanyId == param.CompanyId && x.BranchId == param.BranchId);
                        var TotalPageSize = Math.Ceiling((decimal)resultList.Count() / (int)param.PageSize);
                        param.TotalPageSize = (long)TotalPageSize;
                        return resultList.Skip((int)Page * (int)param.PageSize).Take((int)param.PageSize).ToList();
                    }
                }
                else
                {
                    var resultList = await _stockOpnameRepo.GetStockOpname(param, cancellationToken);
                    return resultList.ToList();
                }
            }

            catch (Exception ex)
            {
                GeneralList._listStockOpname.Clear();
                throw;
            }

        }

        public async Task<List<StockOpnameResponse>> CreateStockOpname(StockOpnameAddRequest param, CancellationToken cancellationToken)
        {
            try
            {
                var resultData = await _stockOpnameRepo.CreateStockOpname(param, cancellationToken);
                if (resultData.Count() > 0)
                {
                    GeneralList._listStockOpname.Add(resultData.First());

                    DailyStockListRequest paramDaileyStock = new();
                    paramDaileyStock.CompanyId = resultData.First().CompanyId;
                    paramDaileyStock.BranchId = resultData.First().BranchId;
                    paramDaileyStock.ProductTypeId = resultData.First().ProductTypeId;
                    paramDaileyStock.ProductId = resultData.First().ProductId;

                    var resultDataDailyStock = await _dailyStockRepo.GetDailyStockList(paramDaileyStock, cancellationToken);
                    var listDataDailyStock = GeneralList._listDailyStock.Where(x => x.CompanyId == paramDaileyStock.CompanyId && x.BranchId == paramDaileyStock.BranchId && x.ProductTypeId == paramDaileyStock.ProductTypeId && x.ProductId == paramDaileyStock.ProductId).FirstOrDefault();
                    if (listDataDailyStock == null) {
                        GeneralList._listDailyStock.Add(resultDataDailyStock.First());
                    } else {
                        listDataDailyStock.StockSell = resultDataDailyStock.First().StockSell;
                        listDataDailyStock.StockSellPrice = resultDataDailyStock.First().StockSellPrice;
                        listDataDailyStock.StockBuy = resultDataDailyStock.First().StockBuy;
                        listDataDailyStock.StockBuyPrice = resultDataDailyStock.First().StockBuyPrice;
                        listDataDailyStock.StockLast = resultDataDailyStock.First().StockLast;
                        listDataDailyStock.UpdateBy = resultDataDailyStock.First().UpdateBy;
                        listDataDailyStock.UpdateDate = DateTime.Now;
                    }

                    MonthlyStockListRequest paramMonthlyStockList = new();
                    paramMonthlyStockList.CompanyId = paramDaileyStock.CompanyId;
                    paramMonthlyStockList.BranchId = paramDaileyStock.BranchId;
                    paramMonthlyStockList.ProductTypeId = paramDaileyStock.ProductTypeId;
                    paramMonthlyStockList.ProductId = paramDaileyStock.ProductId;
                    paramMonthlyStockList.Month = DateTime.Now.Month;
                    paramMonthlyStockList.Year = DateTime.Now.Year;

                    var resultDataMonthlyStock = await _monthlyStockRepo.GetMonthlyStockList(paramMonthlyStockList, cancellationToken);
                    var listDataMonthlyStock = GeneralList._listMonthlyStock.Where(x => x.CompanyId == paramMonthlyStockList.CompanyId && x.BranchId == paramMonthlyStockList.BranchId && x.ProductTypeId == paramMonthlyStockList.ProductTypeId && x.ProductId == paramMonthlyStockList.ProductId && x.Month == paramMonthlyStockList.Month && x.Year == paramMonthlyStockList.Year).FirstOrDefault();
                    if (listDataMonthlyStock == null) {
                        GeneralList._listMonthlyStock.Add(resultDataMonthlyStock.First());
                    } else {
                        listDataMonthlyStock.StockSell = resultDataMonthlyStock.First().StockSell;
                        listDataMonthlyStock.StockSellPrice = resultDataMonthlyStock.First().StockSellPrice;
                        listDataMonthlyStock.StockBuy = resultDataMonthlyStock.First().StockBuy;
                        listDataMonthlyStock.StockBuyPrice = resultDataMonthlyStock.First().StockBuyPrice;
                        listDataMonthlyStock.StockLast = resultDataMonthlyStock.First().StockLast;
                        listDataMonthlyStock.UpdateBy = resultDataMonthlyStock.First().UpdateBy;
                        listDataMonthlyStock.UpdateDate = DateTime.Now;
                    }

                    return resultData.ToList();
                }
                else
                {
                    return resultData.ToList();
                }
            }
            catch (Exception ex)
            {
                GeneralList._listStockOpname.Clear();
                throw;
            }


        }


    }
}
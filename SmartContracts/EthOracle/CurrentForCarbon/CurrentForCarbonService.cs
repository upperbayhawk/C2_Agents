using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using EthOracle.Contracts.CurrentForCarbon.ContractDefinition;

namespace EthOracle.Contracts.CurrentForCarbon
{
    public partial class CurrentForCarbonService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, CurrentForCarbonDeployment currentForCarbonDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<CurrentForCarbonDeployment>().SendRequestAndWaitForReceiptAsync(currentForCarbonDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, CurrentForCarbonDeployment currentForCarbonDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<CurrentForCarbonDeployment>().SendRequestAsync(currentForCarbonDeployment);
        }

        public static async Task<CurrentForCarbonService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, CurrentForCarbonDeployment currentForCarbonDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, currentForCarbonDeployment, cancellationTokenSource);
            return new CurrentForCarbonService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public CurrentForCarbonService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AddGameEventRequestAsync(AddGameEventFunction addGameEventFunction)
        {
             return ContractHandler.SendRequestAsync(addGameEventFunction);
        }

        public Task<TransactionReceipt> AddGameEventRequestAndWaitForReceiptAsync(AddGameEventFunction addGameEventFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGameEventFunction, cancellationToken);
        }

        public Task<string> AddGameEventRequestAsync(string gameEventID, string gameEventName, string gameEventType, string gameEventStartTime, string gameEventEndTime, string gameEventDuration, string dollarPerPoint, string pointsPerWatt, string pointsPerPercent)
        {
            var addGameEventFunction = new AddGameEventFunction();
                addGameEventFunction.GameEventID = gameEventID;
                addGameEventFunction.GameEventName = gameEventName;
                addGameEventFunction.GameEventType = gameEventType;
                addGameEventFunction.GameEventStartTime = gameEventStartTime;
                addGameEventFunction.GameEventEndTime = gameEventEndTime;
                addGameEventFunction.GameEventDuration = gameEventDuration;
                addGameEventFunction.DollarPerPoint = dollarPerPoint;
                addGameEventFunction.PointsPerWatt = pointsPerWatt;
                addGameEventFunction.PointsPerPercent = pointsPerPercent;
            
             return ContractHandler.SendRequestAsync(addGameEventFunction);
        }

        public Task<TransactionReceipt> AddGameEventRequestAndWaitForReceiptAsync(string gameEventID, string gameEventName, string gameEventType, string gameEventStartTime, string gameEventEndTime, string gameEventDuration, string dollarPerPoint, string pointsPerWatt, string pointsPerPercent, CancellationTokenSource cancellationToken = null)
        {
            var addGameEventFunction = new AddGameEventFunction();
                addGameEventFunction.GameEventID = gameEventID;
                addGameEventFunction.GameEventName = gameEventName;
                addGameEventFunction.GameEventType = gameEventType;
                addGameEventFunction.GameEventStartTime = gameEventStartTime;
                addGameEventFunction.GameEventEndTime = gameEventEndTime;
                addGameEventFunction.GameEventDuration = gameEventDuration;
                addGameEventFunction.DollarPerPoint = dollarPerPoint;
                addGameEventFunction.PointsPerWatt = pointsPerWatt;
                addGameEventFunction.PointsPerPercent = pointsPerPercent;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGameEventFunction, cancellationToken);
        }

        public Task<string> AddGamePlayerRequestAsync(AddGamePlayerFunction addGamePlayerFunction)
        {
             return ContractHandler.SendRequestAsync(addGamePlayerFunction);
        }

        public Task<TransactionReceipt> AddGamePlayerRequestAndWaitForReceiptAsync(AddGamePlayerFunction addGamePlayerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGamePlayerFunction, cancellationToken);
        }

        public Task<string> AddGamePlayerRequestAsync(string gamePlayerID, string playerAddress, string dataConnectionString)
        {
            var addGamePlayerFunction = new AddGamePlayerFunction();
                addGamePlayerFunction.GamePlayerID = gamePlayerID;
                addGamePlayerFunction.PlayerAddress = playerAddress;
                addGamePlayerFunction.DataConnectionString = dataConnectionString;
            
             return ContractHandler.SendRequestAsync(addGamePlayerFunction);
        }

        public Task<TransactionReceipt> AddGamePlayerRequestAndWaitForReceiptAsync(string gamePlayerID, string playerAddress, string dataConnectionString, CancellationTokenSource cancellationToken = null)
        {
            var addGamePlayerFunction = new AddGamePlayerFunction();
                addGamePlayerFunction.GamePlayerID = gamePlayerID;
                addGamePlayerFunction.PlayerAddress = playerAddress;
                addGamePlayerFunction.DataConnectionString = dataConnectionString;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGamePlayerFunction, cancellationToken);
        }

        public Task<string> AddGameResultRequestAsync(AddGameResultFunction addGameResultFunction)
        {
             return ContractHandler.SendRequestAsync(addGameResultFunction);
        }

        public Task<TransactionReceipt> AddGameResultRequestAndWaitForReceiptAsync(AddGameResultFunction addGameResultFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGameResultFunction, cancellationToken);
        }

        public Task<string> AddGameResultRequestAsync(string gamePlayerID, string gameEventID, string gamePlayerAddress, string averagePowerInWatts, string baselineAveragePowerInWatts, string deltaAveragePowerInWatts, string percentPoints, string wattPoints, string totalPointsAwarded, string awardValue)
        {
            var addGameResultFunction = new AddGameResultFunction();
                addGameResultFunction.GamePlayerID = gamePlayerID;
                addGameResultFunction.GameEventID = gameEventID;
                addGameResultFunction.GamePlayerAddress = gamePlayerAddress;
                addGameResultFunction.AveragePowerInWatts = averagePowerInWatts;
                addGameResultFunction.BaselineAveragePowerInWatts = baselineAveragePowerInWatts;
                addGameResultFunction.DeltaAveragePowerInWatts = deltaAveragePowerInWatts;
                addGameResultFunction.PercentPoints = percentPoints;
                addGameResultFunction.WattPoints = wattPoints;
                addGameResultFunction.TotalPointsAwarded = totalPointsAwarded;
                addGameResultFunction.AwardValue = awardValue;
            
             return ContractHandler.SendRequestAsync(addGameResultFunction);
        }

        public Task<TransactionReceipt> AddGameResultRequestAndWaitForReceiptAsync(string gamePlayerID, string gameEventID, string gamePlayerAddress, string averagePowerInWatts, string baselineAveragePowerInWatts, string deltaAveragePowerInWatts, string percentPoints, string wattPoints, string totalPointsAwarded, string awardValue, CancellationTokenSource cancellationToken = null)
        {
            var addGameResultFunction = new AddGameResultFunction();
                addGameResultFunction.GamePlayerID = gamePlayerID;
                addGameResultFunction.GameEventID = gameEventID;
                addGameResultFunction.GamePlayerAddress = gamePlayerAddress;
                addGameResultFunction.AveragePowerInWatts = averagePowerInWatts;
                addGameResultFunction.BaselineAveragePowerInWatts = baselineAveragePowerInWatts;
                addGameResultFunction.DeltaAveragePowerInWatts = deltaAveragePowerInWatts;
                addGameResultFunction.PercentPoints = percentPoints;
                addGameResultFunction.WattPoints = wattPoints;
                addGameResultFunction.TotalPointsAwarded = totalPointsAwarded;
                addGameResultFunction.AwardValue = awardValue;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addGameResultFunction, cancellationToken);
        }

        public Task<bool> GameEventExistsQueryAsync(GameEventExistsFunction gameEventExistsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GameEventExistsFunction, bool>(gameEventExistsFunction, blockParameter);
        }

        
        public Task<bool> GameEventExistsQueryAsync(byte[] gameEventIDHash, BlockParameter blockParameter = null)
        {
            var gameEventExistsFunction = new GameEventExistsFunction();
                gameEventExistsFunction.GameEventIDHash = gameEventIDHash;
            
            return ContractHandler.QueryAsync<GameEventExistsFunction, bool>(gameEventExistsFunction, blockParameter);
        }

        public Task<bool> GamePlayerExistsQueryAsync(GamePlayerExistsFunction gamePlayerExistsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GamePlayerExistsFunction, bool>(gamePlayerExistsFunction, blockParameter);
        }

        
        public Task<bool> GamePlayerExistsQueryAsync(byte[] gamePlayerIDHash, BlockParameter blockParameter = null)
        {
            var gamePlayerExistsFunction = new GamePlayerExistsFunction();
                gamePlayerExistsFunction.GamePlayerIDHash = gamePlayerIDHash;
            
            return ContractHandler.QueryAsync<GamePlayerExistsFunction, bool>(gamePlayerExistsFunction, blockParameter);
        }

        public Task<bool> GameResultExistsQueryAsync(GameResultExistsFunction gameResultExistsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GameResultExistsFunction, bool>(gameResultExistsFunction, blockParameter);
        }

        
        public Task<bool> GameResultExistsQueryAsync(byte[] gameResultIDHash, BlockParameter blockParameter = null)
        {
            var gameResultExistsFunction = new GameResultExistsFunction();
                gameResultExistsFunction.GameResultIDHash = gameResultIDHash;
            
            return ContractHandler.QueryAsync<GameResultExistsFunction, bool>(gameResultExistsFunction, blockParameter);
        }

        public Task<bool> HasResultReadyFlagQueryAsync(HasResultReadyFlagFunction hasResultReadyFlagFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasResultReadyFlagFunction, bool>(hasResultReadyFlagFunction, blockParameter);
        }

        
        public Task<bool> HasResultReadyFlagQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<HasResultReadyFlagFunction, bool>(null, blockParameter);
        }

        public Task<string> ContractNameQueryAsync(ContractNameFunction contractNameFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ContractNameFunction, string>(contractNameFunction, blockParameter);
        }

        
        public Task<string> ContractNameQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ContractNameFunction, string>(null, blockParameter);
        }

        public Task<string> AddTestDataRequestAsync(AddTestDataFunction addTestDataFunction)
        {
             return ContractHandler.SendRequestAsync(addTestDataFunction);
        }

        public Task<string> AddTestDataRequestAsync()
        {
             return ContractHandler.SendRequestAsync<AddTestDataFunction>();
        }

        public Task<TransactionReceipt> AddTestDataRequestAndWaitForReceiptAsync(AddTestDataFunction addTestDataFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addTestDataFunction, cancellationToken);
        }

        public Task<TransactionReceipt> AddTestDataRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<AddTestDataFunction>(null, cancellationToken);
        }

        public Task<string> GetAddressQueryAsync(GetAddressFunction getAddressFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddressFunction, string>(getAddressFunction, blockParameter);
        }

        
        public Task<string> GetAddressQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetAddressFunction, string>(null, blockParameter);
        }

        public Task<string> KillRequestAsync(KillFunction killFunction)
        {
             return ContractHandler.SendRequestAsync(killFunction);
        }

        public Task<string> KillRequestAsync()
        {
             return ContractHandler.SendRequestAsync<KillFunction>();
        }

        public Task<TransactionReceipt> KillRequestAndWaitForReceiptAsync(KillFunction killFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(killFunction, cancellationToken);
        }

        public Task<TransactionReceipt> KillRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<KillFunction>(null, cancellationToken);
        }

        public Task<string> OwnerQueryAsync(OwnerFunction ownerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(ownerFunction, blockParameter);
        }

        
        public Task<string> OwnerQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<OwnerFunction, string>(null, blockParameter);
        }

        public Task<bool> TestConnectionQueryAsync(TestConnectionFunction testConnectionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TestConnectionFunction, bool>(testConnectionFunction, blockParameter);
        }

        
        public Task<bool> TestConnectionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TestConnectionFunction, bool>(null, blockParameter);
        }

        public Task<string> TransferOwnershipRequestAsync(TransferOwnershipFunction transferOwnershipFunction)
        {
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(TransferOwnershipFunction transferOwnershipFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }

        public Task<string> TransferOwnershipRequestAsync(string newOwner)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAsync(transferOwnershipFunction);
        }

        public Task<TransactionReceipt> TransferOwnershipRequestAndWaitForReceiptAsync(string newOwner, CancellationTokenSource cancellationToken = null)
        {
            var transferOwnershipFunction = new TransferOwnershipFunction();
                transferOwnershipFunction.NewOwner = newOwner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(transferOwnershipFunction, cancellationToken);
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace EthOracle.Contracts.CurrentForCarbon.ContractDefinition
{


    public partial class CurrentForCarbonDeployment : CurrentForCarbonDeploymentBase
    {
        public CurrentForCarbonDeployment() : base(BYTECODE) { }
        public CurrentForCarbonDeployment(string byteCode) : base(byteCode) { }
    }

    public class CurrentForCarbonDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "60806040526008805460ff191690553480156200001b57600080fd5b50600080546001600160a01b0319163317905560408051606081019091526021808252620016f5602083013980516200005d9160079160209091019062000064565b5062000100565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10620000a757805160ff1916838001178555620000d7565b82800160010185558215620000d7579182015b82811115620000d7578251825591602001919060010190620000ba565b50620000e5929150620000e9565b5090565b5b80821115620000e55760008155600101620000ea565b6115e580620001106000396000f3fe608060405234801561001057600080fd5b50600436106100ea5760003560e01c80637954fd321161008c578063a212b45411610066578063a212b454146101a8578063c7604ab6146101b0578063e0b5cca9146101c3578063f2fde38b146101cb576100ea565b80637954fd321461017a5780638a5ed70b1461018d5780638da5cb5b146101a0576100ea565b806351df2a12116100c857806351df2a121461012c578063584625111461013f5780636625f6581461015257806369ab83be14610165576100ea565b80633412a15c146100ef57806338cc48311461010d57806341c0e1b514610122575b600080fd5b6100f76101de565b6040516101049190611380565b60405180910390f35b6101156101e3565b604051610104919061136c565b61012a6101e7565b005b6100f761013a366004610f2a565b610201565b61012a61014d366004610fda565b61022b565b61012a610160366004611148565b61058d565b61016d610883565b604051610104919061138b565b6100f7610188366004610f2a565b610911565b6100f761019b366004610f2a565b610938565b61011561095f565b61012a61096e565b61012a6101be366004610f5a565b610987565b6100f7610c26565b61012a6101d9366004610f08565b610c2f565b600190565b3090565b6000546001600160a01b031633146101fe57600080fd5b33ff5b60035460009061021357506000610226565b5060008181526004602052604090205415155b919050565b600060028b8b60405160200161024292919061133d565b60408051601f198184030181529082905261025c916112d5565b602060405180830381855afa158015610279573d6000803e3d6000fd5b5050506040513d601f19601f8201168201806040525081019061029c9190610f42565b90506102a781610938565b156102b157600080fd5b6102b9610cb4565b604081018290528b815260208082018c9052606082018d9052608082018c90526001600160a01b038b1660a083015260e082018a9052610100820189905261012082018890526101408201879052610160820186905261018082018590526101a0820184905260006101c08301819052600580546001810182559152825180518493600f9093027f036b6384b5eca791c62761152d0c79bb0604c104a5fb6f4eb0703f3154bb3db00192610371928492910190610d3b565b50602082810151805161038a9260018501920190610d3b565b5060408201516002820155606082015180516103b0916003840191602090910190610d3b565b50608082015180516103cc916004840191602090910190610d3b565b5060a08201516005820180546001600160a01b0319166001600160a01b0390921691909117905560c0820151600682015560e08201518051610418916007840191602090910190610d3b565b506101008201518051610435916008840191602090910190610d3b565b506101208201518051610452916009840191602090910190610d3b565b50610140820151805161046f91600a840191602090910190610d3b565b50610160820151805161048c91600b840191602090910190610d3b565b5061018082015180516104a991600c840191602090910190610d3b565b506101a082015180516104c691600d840191602090910190610d3b565b506101c09190910151600e909101805460ff191691151591909117905560055460008381526006602090815260409182902060001990930192839055830151905161051191906112d5565b6040519081900381208351909161052891906112d5565b60405180910390207f6cf1b3ee9b366cf34f41d434689d9453ace1ab010fbe841c938d12d6d7c665988f8f8e8e8e8e8e8e8e8d6101c001516040516105769a999897969594939291906114aa565b60405180910390a350505050505050505050505050565b600060028a6040516020016105a291906112d5565b60408051601f19818403018152908290526105bc916112d5565b602060405180830381855afa1580156105d9573d6000803e3d6000fd5b5050506040513d601f19601f820116820180604052508101906105fc9190610f42565b905061060781610911565b1561061157600080fd5b610619610db9565b606081018290528a815260208082018b9052604082018c9052608082018b905260a082018a905260c0820189905260e08201889052610100820187905261012082018690526101408201859052610160820184905260038054600181018255600091909152825180518493600c9093027fc2575a0e9e593c00f959f8c92f12db2869c3395a3b0502d05e2516446f71f85b01926106ba928492910190610d3b565b5060208281015180516106d39260018501920190610d3b565b50604082015180516106ef916002840191602090910190610d3b565b506060820151600382015560808201518051610715916004840191602090910190610d3b565b5060a08201518051610731916005840191602090910190610d3b565b5060c0820151805161074d916006840191602090910190610d3b565b5060e08201518051610769916007840191602090910190610d3b565b506101008201518051610786916008840191602090910190610d3b565b5061012082015180516107a3916009840191602090910190610d3b565b5061014082015180516107c091600a840191602090910190610d3b565b5061016082015180516107dd91600b840191602090910190610d3b565b505060035460008481526004602090815260409182902060001990930192839055840151905191925061080f916112d5565b6040519081900381208351909161082691906112d5565b60405180910390207f1c09db5462168b8fe4246eb51c1d03def28bdeec873f0ded0b018e31e2e444288e8e8e8e8e8e8e8e8e60405161086d999897969594939291906113e5565b60405180910390a3505050505050505050505050565b6007805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156109095780601f106108de57610100808354040283529160200191610909565b820191906000526020600020905b8154815290600101906020018083116108ec57829003601f168201915b505050505081565b60015460009061092357506000610226565b50600090815260026020526040902054151590565b60055460009061094a57506000610226565b50600090815260066020526040902054151590565b6000546001600160a01b031681565b6000546001600160a01b0316331461098557600080fd5b565b600060028284866040516020016109a0939291906112f1565b60408051601f19818403018152908290526109ba916112d5565b602060405180830381855afa1580156109d7573d6000803e3d6000fd5b5050506040513d601f19601f820116820180604052508101906109fa9190610f42565b90506000600285604051602001610a1191906112d5565b60408051601f1981840301815290829052610a2b916112d5565b602060405180830381855afa158015610a48573d6000803e3d6000fd5b5050506040513d601f19601f82011682018060405250810190610a6b9190610f42565b9050610a7681610911565b15610a8057600080fd5b610a88610e1d565b8581526020808201879052606082018590526001600160a01b038616608083015260408201849052600160c083018190528054808201825560009190915282518051849360059093027fb10e2d527612073b26eecdfd717e6a320cf44b4afac2b0732d9fcbe2b7fa0cf60192610b02928492910190610d3b565b506020828101518051610b1b9260018501920190610d3b565b506040820151600282015560608201518051610b41916003840191602090910190610d3b565b5060808201516004909101805460a084015160c0909401511515600160a81b0260ff60a81b19941515600160a01b0260ff60a01b196001600160a01b039095166001600160a01b031990931692909217939093161792909216179055600154600083815260026020526040908190206000199092019182905582519051610bc891906112d5565b60405180910390207f460901c2e1d010e289193dbfeee389ea47c8947a6aa111430e7d22acc41c377b8360200151846060015185608001518660c00151604051610c15949392919061139e565b60405180910390a250505050505050565b60085460ff1681565b6000546001600160a01b03163314610c4657600080fd5b6001600160a01b038116610c5957600080fd5b600080546040516001600160a01b03808516939216917f8be0079c531659141344cd1fd0a4f28419497f9722a3daafe3b4186f6b6457e091a3600080546001600160a01b0319166001600160a01b0392909216919091179055565b604051806101e00160405280606081526020016060815260200160008019168152602001606081526020016060815260200160006001600160a01b0316815260200160008019168152602001606081526020016060815260200160608152602001606081526020016060815260200160608152602001606081526020016000151581525090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f10610d7c57805160ff1916838001178555610da9565b82800160010185558215610da9579182015b82811115610da9578251825591602001919060010190610d8e565b50610db5929150610e58565b5090565b6040518061018001604052806060815260200160608152602001606081526020016000801916815260200160608152602001606081526020016060815260200160608152602001606081526020016060815260200160608152602001606081525090565b6040805160e0810182526060808252602082018190526000928201839052808201526080810182905260a0810182905260c081019190915290565b5b80821115610db55760008155600101610e59565b80356001600160a01b0381168114610e8457600080fd5b92915050565b600082601f830112610e9a578081fd5b813567ffffffffffffffff80821115610eb1578283fd5b604051601f8301601f191681016020018281118282101715610ed1578485fd5b604052828152925082848301602001861015610eec57600080fd5b8260208601602083013760006020848301015250505092915050565b600060208284031215610f19578081fd5b610f238383610e6d565b9392505050565b600060208284031215610f3b578081fd5b5035919050565b600060208284031215610f53578081fd5b5051919050565b600080600060608486031215610f6e578182fd5b833567ffffffffffffffff80821115610f85578384fd5b610f9187838801610e8a565b9450602086013591506001600160a01b0382168214610fae578384fd5b90925060408501359080821115610fc3578283fd5b50610fd086828701610e8a565b9150509250925092565b6000806000806000806000806000806101408b8d031215610ff9578586fd5b8a3567ffffffffffffffff80821115611010578788fd5b61101c8e838f01610e8a565b9b5060208d0135915080821115611031578788fd5b61103d8e838f01610e8a565b9a5061104c8e60408f01610e6d565b995060608d0135915080821115611061578788fd5b61106d8e838f01610e8a565b985060808d0135915080821115611082578788fd5b61108e8e838f01610e8a565b975060a08d01359150808211156110a3578687fd5b6110af8e838f01610e8a565b965060c08d01359150808211156110c4578586fd5b6110d08e838f01610e8a565b955060e08d01359150808211156110e5578485fd5b6110f18e838f01610e8a565b94506101008d0135915080821115611107578384fd5b6111138e838f01610e8a565b93506101208d0135915080821115611129578283fd5b506111368d828e01610e8a565b9150509295989b9194979a5092959850565b60008060008060008060008060006101208a8c031215611166578485fd5b893567ffffffffffffffff8082111561117d578687fd5b6111898d838e01610e8a565b9a5060208c013591508082111561119e578687fd5b6111aa8d838e01610e8a565b995060408c01359150808211156111bf578687fd5b6111cb8d838e01610e8a565b985060608c01359150808211156111e0578687fd5b6111ec8d838e01610e8a565b975060808c0135915080821115611201578687fd5b61120d8d838e01610e8a565b965060a08c0135915080821115611222578586fd5b61122e8d838e01610e8a565b955060c08c0135915080821115611243578485fd5b61124f8d838e01610e8a565b945060e08c0135915080821115611264578384fd5b6112708d838e01610e8a565b93506101008c0135915080821115611286578283fd5b506112938c828d01610e8a565b9150509295985092959850929598565b15159052565b600081518084526112c181602086016020860161157f565b601f01601f19169290920160200192915050565b600082516112e781846020870161157f565b9190910192915050565b6000845161130381846020890161157f565b606085901b6bffffffffffffffffffffffff1916908301908152835161133081601484016020880161157f565b0160140195945050505050565b6000835161134f81846020880161157f565b83519083019061136381836020880161157f565b01949350505050565b6001600160a01b0391909116815260200190565b901515815260200190565b600060208252610f2360208301846112a9565b6000608082526113b160808301876112a9565b82810360208401526113c381876112a9565b6001600160a01b03959095166040840152505090151560609091015292915050565b60006101208083526113f98184018d6112a9565b9050828103602084015261140d818c6112a9565b90508281036040840152611421818b6112a9565b90508281036060840152611435818a6112a9565b9050828103608084015261144981896112a9565b905082810360a084015261145d81886112a9565b905082810360c084015261147181876112a9565b905082810360e084015261148581866112a9565b905082810361010084015261149a81856112a9565b9c9b505050505050505050505050565b60006101408083526114be8184018e6112a9565b905082810360208401526114d2818d6112a9565b905082810360408401526114e6818c6112a9565b905082810360608401526114fa818b6112a9565b9050828103608084015261150e818a6112a9565b905082810360a084015261152281896112a9565b905082810360c084015261153681886112a9565b905082810360e084015261154a81876112a9565b905082810361010084015261155f81866112a9565b9150506115706101208301846112a3565b9b9a5050505050505050505050565b60005b8381101561159a578181015183820152602001611582565b838111156115a9576000848401525b5050505056fea264697066735822122044dfda3eed0921570ce0b73f4b43392d63bb80b427e7a68f181a78045e6f839a64736f6c6343000700003355707065726261792053797374656d732043757272656e74466f72436172626f6e";
        public CurrentForCarbonDeploymentBase() : base(BYTECODE) { }
        public CurrentForCarbonDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddGameEventFunction : AddGameEventFunctionBase { }

    [Function("AddGameEvent")]
    public class AddGameEventFunctionBase : FunctionMessage
    {
        [Parameter("string", "gameEventID", 1)]
        public virtual string GameEventID { get; set; }
        [Parameter("string", "gameEventName", 2)]
        public virtual string GameEventName { get; set; }
        [Parameter("string", "gameEventType", 3)]
        public virtual string GameEventType { get; set; }
        [Parameter("string", "gameEventStartTime", 4)]
        public virtual string GameEventStartTime { get; set; }
        [Parameter("string", "gameEventEndTime", 5)]
        public virtual string GameEventEndTime { get; set; }
        [Parameter("string", "gameEventDuration", 6)]
        public virtual string GameEventDuration { get; set; }
        [Parameter("string", "dollarPerPoint", 7)]
        public virtual string DollarPerPoint { get; set; }
        [Parameter("string", "pointsPerWatt", 8)]
        public virtual string PointsPerWatt { get; set; }
        [Parameter("string", "pointsPerPercent", 9)]
        public virtual string PointsPerPercent { get; set; }
    }

    public partial class AddGamePlayerFunction : AddGamePlayerFunctionBase { }

    [Function("AddGamePlayer")]
    public class AddGamePlayerFunctionBase : FunctionMessage
    {
        [Parameter("string", "gamePlayerID", 1)]
        public virtual string GamePlayerID { get; set; }
        [Parameter("address", "playerAddress", 2)]
        public virtual string PlayerAddress { get; set; }
        [Parameter("string", "dataConnectionString", 3)]
        public virtual string DataConnectionString { get; set; }
    }

    public partial class AddGameResultFunction : AddGameResultFunctionBase { }

    [Function("AddGameResult")]
    public class AddGameResultFunctionBase : FunctionMessage
    {
        [Parameter("string", "gamePlayerID", 1)]
        public virtual string GamePlayerID { get; set; }
        [Parameter("string", "gameEventID", 2)]
        public virtual string GameEventID { get; set; }
        [Parameter("address", "gamePlayerAddress", 3)]
        public virtual string GamePlayerAddress { get; set; }
        [Parameter("string", "averagePowerInWatts", 4)]
        public virtual string AveragePowerInWatts { get; set; }
        [Parameter("string", "baselineAveragePowerInWatts", 5)]
        public virtual string BaselineAveragePowerInWatts { get; set; }
        [Parameter("string", "deltaAveragePowerInWatts", 6)]
        public virtual string DeltaAveragePowerInWatts { get; set; }
        [Parameter("string", "percentPoints", 7)]
        public virtual string PercentPoints { get; set; }
        [Parameter("string", "wattPoints", 8)]
        public virtual string WattPoints { get; set; }
        [Parameter("string", "totalPointsAwarded", 9)]
        public virtual string TotalPointsAwarded { get; set; }
        [Parameter("string", "awardValue", 10)]
        public virtual string AwardValue { get; set; }
    }

    public partial class GameEventExistsFunction : GameEventExistsFunctionBase { }

    [Function("GameEventExists", "bool")]
    public class GameEventExistsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_gameEventIDHash", 1)]
        public virtual byte[] GameEventIDHash { get; set; }
    }

    public partial class GamePlayerExistsFunction : GamePlayerExistsFunctionBase { }

    [Function("GamePlayerExists", "bool")]
    public class GamePlayerExistsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_gamePlayerIDHash", 1)]
        public virtual byte[] GamePlayerIDHash { get; set; }
    }

    public partial class GameResultExistsFunction : GameResultExistsFunctionBase { }

    [Function("GameResultExists", "bool")]
    public class GameResultExistsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "_gameResultIDHash", 1)]
        public virtual byte[] GameResultIDHash { get; set; }
    }

    public partial class HasResultReadyFlagFunction : HasResultReadyFlagFunctionBase { }

    [Function("HasResultReadyFlag", "bool")]
    public class HasResultReadyFlagFunctionBase : FunctionMessage
    {

    }

    public partial class ContractNameFunction : ContractNameFunctionBase { }

    [Function("_contractName", "string")]
    public class ContractNameFunctionBase : FunctionMessage
    {

    }

    public partial class AddTestDataFunction : AddTestDataFunctionBase { }

    [Function("addTestData")]
    public class AddTestDataFunctionBase : FunctionMessage
    {

    }

    public partial class GetAddressFunction : GetAddressFunctionBase { }

    [Function("getAddress", "address")]
    public class GetAddressFunctionBase : FunctionMessage
    {

    }

    public partial class KillFunction : KillFunctionBase { }

    [Function("kill")]
    public class KillFunctionBase : FunctionMessage
    {

    }

    public partial class OwnerFunction : OwnerFunctionBase { }

    [Function("owner", "address")]
    public class OwnerFunctionBase : FunctionMessage
    {

    }

    public partial class TestConnectionFunction : TestConnectionFunctionBase { }

    [Function("testConnection", "bool")]
    public class TestConnectionFunctionBase : FunctionMessage
    {

    }

    public partial class TransferOwnershipFunction : TransferOwnershipFunctionBase { }

    [Function("transferOwnership")]
    public class TransferOwnershipFunctionBase : FunctionMessage
    {
        [Parameter("address", "newOwner", 1)]
        public virtual string NewOwner { get; set; }
    }

    public partial class GameEventEventEventDTO : GameEventEventEventDTOBase { }

    [Event("GameEventEvent")]
    public class GameEventEventEventDTOBase : IEventDTO
    {
        [Parameter("string", "gameEventIDIdx", 1, true )]
        public virtual string GameEventIDIdx { get; set; }
        [Parameter("string", "gameEventNameIdx", 2, true )]
        public virtual string GameEventNameIdx { get; set; }
        [Parameter("string", "gameEventID", 3, false )]
        public virtual string GameEventID { get; set; }
        [Parameter("string", "gameEventName", 4, false )]
        public virtual string GameEventName { get; set; }
        [Parameter("string", "gameEventType", 5, false )]
        public virtual string GameEventType { get; set; }
        [Parameter("string", "gameEventStartTime", 6, false )]
        public virtual string GameEventStartTime { get; set; }
        [Parameter("string", "gameEventEndTime", 7, false )]
        public virtual string GameEventEndTime { get; set; }
        [Parameter("string", "gameEventDuration", 8, false )]
        public virtual string GameEventDuration { get; set; }
        [Parameter("string", "dollarPerPoint", 9, false )]
        public virtual string DollarPerPoint { get; set; }
        [Parameter("string", "pointsPerWatt", 10, false )]
        public virtual string PointsPerWatt { get; set; }
        [Parameter("string", "pointsPerPercent", 11, false )]
        public virtual string PointsPerPercent { get; set; }
    }

    public partial class GamePlayerEventEventDTO : GamePlayerEventEventDTOBase { }

    [Event("GamePlayerEvent")]
    public class GamePlayerEventEventDTOBase : IEventDTO
    {
        [Parameter("string", "gamePlayerIDIdx", 1, true )]
        public virtual string GamePlayerIDIdx { get; set; }
        [Parameter("string", "gamePlayerID", 2, false )]
        public virtual string GamePlayerID { get; set; }
        [Parameter("string", "dataConnectionString", 3, false )]
        public virtual string DataConnectionString { get; set; }
        [Parameter("address", "gamePlayerAddress", 4, false )]
        public virtual string GamePlayerAddress { get; set; }
        [Parameter("bool", "active", 5, false )]
        public virtual bool Active { get; set; }
    }

    public partial class GameResultEventEventDTO : GameResultEventEventDTOBase { }

    [Event("GameResultEvent")]
    public class GameResultEventEventDTOBase : IEventDTO
    {
        [Parameter("string", "gamePlayerIDIdx", 1, true )]
        public virtual string GamePlayerIDIdx { get; set; }
        [Parameter("string", "gameEventIDIdx", 2, true )]
        public virtual string GameEventIDIdx { get; set; }
        [Parameter("string", "gamePlayerID", 3, false )]
        public virtual string GamePlayerID { get; set; }
        [Parameter("string", "gameEventID", 4, false )]
        public virtual string GameEventID { get; set; }
        [Parameter("string", "averagePowerInWatts", 5, false )]
        public virtual string AveragePowerInWatts { get; set; }
        [Parameter("string", "baselineAveragePowerInWatts", 6, false )]
        public virtual string BaselineAveragePowerInWatts { get; set; }
        [Parameter("string", "deltaAveragePowerInWatts", 7, false )]
        public virtual string DeltaAveragePowerInWatts { get; set; }
        [Parameter("string", "percentPoints", 8, false )]
        public virtual string PercentPoints { get; set; }
        [Parameter("string", "wattPoints", 9, false )]
        public virtual string WattPoints { get; set; }
        [Parameter("string", "totalPointsAwarded", 10, false )]
        public virtual string TotalPointsAwarded { get; set; }
        [Parameter("string", "awardValue", 11, false )]
        public virtual string AwardValue { get; set; }
        [Parameter("bool", "confirmed", 12, false )]
        public virtual bool Confirmed { get; set; }
    }

    public partial class OwnershipTransferredEventDTO : OwnershipTransferredEventDTOBase { }

    [Event("OwnershipTransferred")]
    public class OwnershipTransferredEventDTOBase : IEventDTO
    {
        [Parameter("address", "previousOwner", 1, true )]
        public virtual string PreviousOwner { get; set; }
        [Parameter("address", "newOwner", 2, true )]
        public virtual string NewOwner { get; set; }
    }







    public partial class GameEventExistsOutputDTO : GameEventExistsOutputDTOBase { }

    [FunctionOutput]
    public class GameEventExistsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GamePlayerExistsOutputDTO : GamePlayerExistsOutputDTOBase { }

    [FunctionOutput]
    public class GamePlayerExistsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class GameResultExistsOutputDTO : GameResultExistsOutputDTOBase { }

    [FunctionOutput]
    public class GameResultExistsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class HasResultReadyFlagOutputDTO : HasResultReadyFlagOutputDTOBase { }

    [FunctionOutput]
    public class HasResultReadyFlagOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }

    public partial class ContractNameOutputDTO : ContractNameOutputDTOBase { }

    [FunctionOutput]
    public class ContractNameOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class GetAddressOutputDTO : GetAddressOutputDTOBase { }

    [FunctionOutput]
    public class GetAddressOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }



    public partial class OwnerOutputDTO : OwnerOutputDTOBase { }

    [FunctionOutput]
    public class OwnerOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TestConnectionOutputDTO : TestConnectionOutputDTOBase { }

    [FunctionOutput]
    public class TestConnectionOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }


}

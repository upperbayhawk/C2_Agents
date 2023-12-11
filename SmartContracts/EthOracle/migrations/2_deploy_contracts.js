//var CFCToken = artifacts.require("./CFCToken.sol");
var Ownable = artifacts.require("./C2COwnable.sol");
var DateLib = artifacts.require("./DateLib.sol");
//var DataPumpOracle = artifacts.require("./DataPumpOracle.sol");
var CurrentForCarbon = artifacts.require("./CurrentForCarbon.sol");

module.exports = function(deployer) {
    //deployer.deploy(CFCToken);
    deployer.deploy(Ownable);
    //deployer.link(Ownable, DataPumpOracle);
    deployer.link(Ownable, CurrentForCarbon);
	deployer.deploy(DateLib);
	//deployer.link(DateLib, DataPumpOracle);
	deployer.link(DateLib, CurrentForCarbon);
    //deployer.deploy(DataPumpOracle);
    deployer.deploy(CurrentForCarbon);
};





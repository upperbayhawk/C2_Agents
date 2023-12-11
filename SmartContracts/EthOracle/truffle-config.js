/*
 * NB: since truffle-hdwallet-provider 0.0.5 you must wrap HDWallet providers in a 
 * function when declaring them. Failure to do so will cause commands to hang. ex:
 * ```
 * mainnet: {
 *     provider: function() { 
 *       return new HDWalletProvider(mnemonic, 'https://mainnet.infura.io/<infura-key>') 
 *     },
 *     network_id: '1',
 *     gas: 4500000,
 *     gasPrice: 10000000000,
 *   },
 */


const HDWalletProvider = require("@truffle/hdwallet-provider");

const mnemonicPhrase = "shy permit grace repair kind jealous civil divert draft seat penalty learn";
//const futuraProjectID = "6c78b6cf1a304229a1d5a70e6febb2e5";
// project secret 04effdd2b5d645e1a3a6daef1d555413
//0x00E9395cf9c9FEd427110725BDaD40fDDA60fa8A
// goerli account 0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed
//127.0.0.1

module.exports = {
	networks: {
		development: {
		  host: "10.0.2.15",
		  port: 8545,
		  network_id: "*" // Match any network id
		},
        goerli: {
            provider: function () {
                return new HDWalletProvider(mnemonicPhrase, "https://goerli.infura.io/v3/6c78b6cf1a304229a1d5a70e6febb2e5")
			},
            network_id: '5', // eslint-disable-line camelcase
            gas: 4465030,
            gasPrice: 10000000000,
        },
		sepolia: {
            provider: function () {
                return new HDWalletProvider(mnemonicPhrase, "https://sepolia.infura.io/v3/6c78b6cf1a304229a1d5a70e6febb2e5")
			},
            network_id: '11155111', // eslint-disable-line camelcase
            gas: 4465030,
            gasPrice: 10000000000,
        },//goerli: {
        //    provider: () =>
        //        new HDWalletProvider({
        //            mnemonic: {
        //                phrase: "6c78b6cf1a304229a1d5a70e6febb2e5"
        //            },
        //            providerOrUrl: "https://goerli.infura.io/v3/6c78b6cf1a304229a1d5a70e6febb2e5",
        //            numberOfAddresses: 1,
        //            shareNonce: true,
        //            derivationPath: "m/44'/60'/0'/0/"
        //        }),
        //    network_id: '5', // eslint-disable-line camelcase
        //    gas: 4465030,
        //    gasPrice: 10000000000,
        //},
  //      goerli: {
		//	provider: function() {
		//	  return new HDWalletProvider(process.env.MNEMONIC, 'https://goerli.infura.io/v3/' + process.env.INFURA_API_KEY)
		//	},
		//	network_id: '5', // eslint-disable-line camelcase
		//	gas: 4465030,
		//	gasPrice: 10000000000,
		//},
		ropsten: {
			provider: function() {
			  return new HDWalletProvider(process.env.MNENOMIC, "https://ropsten.infura.io/v3/6c78b6cf1a304229a1d5a70e6febb2e5")
			},
			network_id: '3',
			gas: 4465030,
			gasPrice: 10000000000,
		},
		kovan: {
			provider: function() {
			  return new HDWalletProvider(process.env.MNENOMIC, 'https://kovan.infura.io/v3/' + process.env.INFURA_API_KEY)
			},
			network_id: '42',
			gas: 4465030,
			gasPrice: 10000000000,
		},
		volta: {
			provider: function() {
			  //return new HDWalletProvider(mnemonicPhrase, "https://volta-rpc.energyweb.org")
			  return new HDWalletProvider(mnemonicPhrase, "http://192.168.0.109:8545")
			  //return new HDWalletProvider("0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f", "http://192.168.0.109:8545")
			},
			//from: "0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f",
			//host: "192.168.0.109",
			//port: 8545,
			network_id: '73799',
			gas: 4465030,
			gasPrice: 30000000,
		},
		ewc: {
			provider: function() {
			  return new HDWalletProvider(mnemonicPhrase, "https://rpc.energyweb.org")
			},
			network_id: '0xf6',
			gas: 4465030,
			gasPrice: 30000000,
		},
		rinkeby: {
			provider: function() {
				return new HDWalletProvider(mnemonicPhrase, "https://rinkeby.infura.io/v3/6c78b6cf1a304229a1d5a70e6febb2e5");
			},
			network_id: 4,
			gas: 3000000,
			gasPrice: 10000000000
		},// main ethereum network(mainnet)
		//rinkeby: {
		//	provider: function() {
		//		return new HDWalletProvider(process.env.MNENOMIC, "https://rinkeby.infura.io/v3/" + process.env.INFURA_API_KEY)
		//	},
		//	network_id: 4,
		//	gas: 3000000,
		//	gasPrice: 10000000000
		//},// main ethereum network(mainnet)
		main: {
			provider: function() { 
				return new HDWalletProvider(process.env.MNENOMIC, "https://mainnet.infura.io/v3/" + process.env.INFURA_API_KEY)
			},
			network_id: 1,
			gas: 3000000,
			gasPrice: 10000000000
		}
	},
	compilers: {
		solc: {
		  version: "^0.8.17"// A version or constraint - Ex. "^0.5.0"
							 // Can also be set to "native" to use a native solc
		//   docker: <boolean>, // Use a version obtained through docker
		//   parser: "solcjs",  // Leverages solc-js purely for speedy parsing
		//   settings: {
		// 	optimizer: {
		// 	  enabled: <boolean>,
		// 	  runs: <number>   // Optimize for how many times you intend to run the code
		// 	},
		// 	evmVersion: <string> // Default: "petersburg"
		//  }
		}
	  }
	};

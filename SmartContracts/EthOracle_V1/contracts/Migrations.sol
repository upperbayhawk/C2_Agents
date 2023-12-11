pragma solidity >=0.4.21 <0.8.7;
//"SPDX-License-Identifier: UNLICENSED" for non-open-source code. Please see https://spdx.org for more information.

contract Migrations {
  address public owner;
  uint public last_completed_migration;

  constructor()  {
    owner = msg.sender;
  }

  modifier restricted() {
    if (msg.sender == owner) _;
  }

  function setCompleted(uint completed) public restricted {
    last_completed_migration = completed;
  }

  function upgrade(address new_address) public restricted {
    Migrations upgraded = Migrations(new_address);
    upgraded.setCompleted(last_completed_migration);
  }
}

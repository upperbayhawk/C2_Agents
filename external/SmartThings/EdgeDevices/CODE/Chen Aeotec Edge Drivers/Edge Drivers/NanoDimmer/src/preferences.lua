-- Copyright 2021 SmartThings
--
-- Licensed under the Apache License, Version 2.0 (the "License");
-- you may not use this file except in compliance with the License.
-- You may obtain a copy of the License at
--
--     http://www.apache.org/licenses/LICENSE-2.0
--
-- Unless required by applicable law or agreed to in writing, software
-- distributed under the License is distributed on an "AS IS" BASIS,
-- WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
-- See the License for the specific language governing permissions and
-- limitations under the License.

local SWITCH = {
  PARAMETERS = {
    overloadProtection        = { type = 'config', parameter_number = 3, size = 1 },
	overheatProtection        = { type = 'config', parameter_number = 4, size = 1 },
    ledAfterPower     = { type = 'config', parameter_number = 20, size = 1 },
    autoReportType		= {  type = 'config', parameter_number = 80, size = 1  },
	s1Control		= {  type = 'config', parameter_number = 81, size = 1  },
	s2Control		= {  type = 'config', parameter_number = 82, size = 1  },
	powerThreshold       = { type = 'config', parameter_number = 90, size = 1 },
	wattThreshold       = { type = 'config', parameter_number = 91, size = 2 },
	percentageThreshold       = { type = 'config', parameter_number = 92, size = 1 },
	group1Sensors          = { type = 'config', parameter_number = 101, size = 4 },
    group1Time              = { type = 'config', parameter_number = 111, size = 4 },
	s1External    = { type = 'config', parameter_number = 120, size = 1 },
	s2External    = { type = 'config', parameter_number = 121, size = 1 },
	s1Setting    = { type = 'config', parameter_number = 123, size = 1 },
	s2Setting    = { type = 'config', parameter_number = 124, size = 1 },
	dimSpeed    = { type = 'config', parameter_number = 125, size = 1 },
	dimType    = { type = 'config', parameter_number = 129, size = 1 },
	minDim    = { type = 'config', parameter_number = 131, size = 1 },
	maxDim    = { type = 'config', parameter_number = 132, size = 1 },
	loadDetection    = { type = 'config', parameter_number = 249, size = 1 },
    assocGroup1          = { type = 'assoc', group = 1, maxnodes = 5, addhub = false },
	assocGroup2          = { type = 'assoc', group = 1, maxnodes = 5, addhub = false },
	assocGroup3          = { type = 'assoc', group = 1, maxnodes = 5, addhub = false },
	assocGroup4          = { type = 'assoc', group = 1, maxnodes = 5, addhub = false }
  }
}


local devices = {
  AEOTEC_NANO_DIMMER_EU = {
    MATCHING_MATRIX = {
      mfrs = 0x0086,
	  product_types = 0x0003,
      product_ids = 0x006F
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_NANO_DIMMER_US = {
    MATCHING_MATRIX = {
      mfrs = 0x0086,
	  product_types = 0x0103,
      product_ids = 0x006F
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_NANO_DIMMER_AU = {
    MATCHING_MATRIX = {
      mfrs = 0x0086,
	  product_types = 0x0203,
      product_ids = 0x006F
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
}

local preferences = {}

preferences.get_device_parameters = function(zw_device)
  for _, device in pairs(devices) do
    if zw_device:id_match(
      device.MATCHING_MATRIX.mfrs,
	  device.MATCHING_MATRIX.product_types,
      device.MATCHING_MATRIX.product_ids) then
      return device.PARAMETERS
    end
  end
  return nil
end

preferences.to_numeric_value = function(new_value)
  local numeric = tonumber(new_value)
  if numeric == nil then -- in case the value is boolean
    numeric = new_value and 1 or 0
  end
  return numeric
end

return preferences
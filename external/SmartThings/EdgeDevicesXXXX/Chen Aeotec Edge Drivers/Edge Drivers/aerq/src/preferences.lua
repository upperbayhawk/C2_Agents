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
	selectiveTemp          = { type = 'config', parameter_number = 1, size = 1 },
	selectiveHum           = { type = 'config', parameter_number = 2, size = 1 },
	selectiveTime          = { type = 'config', parameter_number = 3, size = 1 },
	periodicTime          = { type = 'config', parameter_number = 4, size = 2 },
	offsetTemp              = { type = 'config', parameter_number = 14, size = 2 },
	offsetHum              = { type = 'config', parameter_number = 15, size = 1 },
	waterTime			= { type = 'config', parameter_number = 16, size = 1 },
    assocGroup1          = { type = 'assoc', group = 1, maxnodes = 5, addhub = true },
	assocGroup2          = { type = 'assoc', group = 2, maxnodes = 5, addhub = false },
	assocGroup3          = { type = 'assoc', group = 3, maxnodes = 5, addhub = false },
	assocGroup4          = { type = 'assoc', group = 4, maxnodes = 5, addhub = false },
	assocGroup5          = { type = 'assoc', group = 5, maxnodes = 5, addhub = false },
	assocGroup6          = { type = 'assoc', group = 6, maxnodes = 5, addhub = false }
  }
}


local devices = {
  AEOTEC_AERQ_EU = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0002,
      product_ids = 0x0009
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_AERQ_US = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0102,
      product_ids = 0x0009
    },
    PARAMETERS = SWITCH.PARAMETERS
  },
  AEOTEC_AERQ_AU = {
    MATCHING_MATRIX = {
      mfrs = 0x0371,
	  product_types = 0x0202,
      product_ids = 0x0009
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